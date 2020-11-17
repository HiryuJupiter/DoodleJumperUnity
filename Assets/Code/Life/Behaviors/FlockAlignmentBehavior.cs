using UnityEngine;

namespace AIAssignment3
{
    public class FlockAlignmentBehavior : BehaviorBase
    {
        float weight = 0.5f;

        //Constructor
        public FlockAlignmentBehavior(Lifeform lifeform) : base(lifeform) {}

        public override void TickFixedUpdate()
        {
            Vector2 alignmentDir = Vector2.zero;
            //If we have one neighbor to begin with, then ...
            if (lifeform.neighbors.SameFlock.Count > 0)
            {
                //Get the averaged forward direction of neighbors
                foreach (Transform n in lifeform.neighbors.SameFlock)
                {
                    alignmentDir += (Vector2)n.transform.up;
                }
                alignmentDir = alignmentDir / lifeform.neighbors.SameFlock.Count;
            }
            lifeform.NewMoveDir += alignmentDir * weight;
        }
    }
}