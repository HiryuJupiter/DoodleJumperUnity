using UnityEngine;

namespace AIAssignment3
{
    public class WaypointFollowBehavior : BehaviorBase
    {
        const float Weight = 2f;

        //Variables
        Path path;
        int waypointIndex = -1;
        Vector3 waypointPos;

        //Constructor
        public WaypointFollowBehavior(Lifeform lifeform) : base(lifeform) 
        {
            path = WaypointManager.instance.TestPath;
        }

        public override void TickFixedUpdate()
        {
            //If we don't have a waypoint yet, then find one
            if (waypointIndex == -1)
            {
                FindClosestWayPoint(transform.position);
            }
            Debug.DrawLine(transform.position, waypointPos);

            //If we have arrived at waypoint, then get the next one
            if (HasArrivedAtWaypoint(transform.position))
            {
                GetNextWaypoint();
            }

            lifeform.NewMoveDir += (Vector2)(waypointPos - transform.position).normalized * Weight;
        }

        public void StopFollowingPath()
        {
            //We use -1 to indicate we don't have a waypoint currently
            waypointIndex = -1;
        }

        bool HasArrivedAtWaypoint(Vector3 pos)
        {
            //Use a distance check to see if we've arrived at a destination
            return (Vector2.SqrMagnitude(waypointPos - pos) < 1f);
        }

        void FindClosestWayPoint(Vector3 pos)
        {
            //Use the path class to find closest waypoint
            waypointIndex = path.GetClosestWaypointIndex(pos);
            waypointPos = path.GetWaypointPosition(waypointIndex);
        }

        void GetNextWaypoint()
        {
            //Use a path class method to find the next waypoint
            waypointIndex = path.GetNextIndex(waypointIndex);
            waypointPos = path.GetWaypointPosition(waypointIndex);
        }

    }
}