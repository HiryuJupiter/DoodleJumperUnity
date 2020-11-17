using System.Collections.Generic;
using UnityEngine;

namespace AIAssignment3
{
    [CreateAssetMenu(menuName = "Flock/Behavior/Alignment")]
    public class AlignmentBehavior : FlockBehavior
    {
        public override Vector2 CalculateMove(FlockAgent agent, List<Transform> neighbors, Flock flock)
        {
            //If there is no neighbors to check against, just move forward
            if (neighbors.Count == 0)
                return agent.transform.up;

            //Add all neighbor's forward vector3 together and then average, to get the average forward dir.
            Vector2 alignmentMove = Vector2.zero;
            foreach (Transform item in neighbors)
            {
                alignmentMove += (Vector2)item.transform.up;
            }
            alignmentMove /= neighbors.Count;

            return alignmentMove;
        }
    }
}