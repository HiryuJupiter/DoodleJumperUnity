using UnityEngine;
using System.Collections.Generic;

namespace AIAssignment3
{
    //A class for holding a series of waypoint transforms 
    [System.Serializable]
    public class Path : MonoBehaviour
    {
        [SerializeField] List<Transform> waypoints;

        public int GetClosestWaypointIndex(Vector3 pos)
        {
            //Find the closest index by comparing their distance to the given position
            int closestIndex = 0;
            float closestDist = float.MaxValue;
            for (int i = 0; i < waypoints.Count; i++)
            {
                float dist = Vector2.SqrMagnitude(pos - waypoints[i].position);
                if (dist < closestDist)
                {
                    closestDist = dist;
                    closestIndex = i;
                }
            }
            return closestIndex;
        }

        public Vector3 GetWaypointPosition(int index)
        {
            //Return the waypoint's position based on given index
            if (index < waypoints.Count)
            {
                return waypoints[index].position;
            }
            return waypoints[0].position;
        }

        public int GetNextIndex(int currentIndex)
        {
            //Increment current index. If it goes over the collection size, set it back to zero.
            if (++currentIndex >= waypoints.Count)
            {
                currentIndex = 0;
            }
            return currentIndex;
        }

        void OnDrawGizmos()
        {
            //Visualize the waypoint connection on screen
            for (int i = 0; i < waypoints.Count; i++)
            {
                //Set the visualization's color, draw a box around each node and draw lines inbetween connecting nodes
                Gizmos.color = Color.cyan;
                Gizmos.DrawCube(waypoints[i].position, Vector3.one);

                if (i < waypoints.Count - 1)
                {
                    Gizmos.DrawLine(waypoints[i].position, waypoints[i + 1].position);
                }
            }
        }
    }
}