using UnityEngine;

namespace AIAssignment3
{
    public class SameFlockAvoidanceBehavior : BehaviorBase
    {
        const float weight = 5f;
        float squaredCheckDist;

        //Constructor
        public SameFlockAvoidanceBehavior(Lifeform lifeform) : base(lifeform)
        {
            //Cache calculation
            squaredCheckDist = flock.SmallRadius * flock.SmallRadius;
        }

        public override void TickFixedUpdate()
        {
            //If we have neighbors from the same flock...
            if (lifeform.neighbors.SameFlock.Count > 0)
            {
                Vector2 avoidanceDir = Vector2.zero;

                //Then add up their forward facing direction and then average it
                foreach (Transform n in lifeform.neighbors.SameFlock)
                {
                    if (Vector2.SqrMagnitude(lifeform.transform.position - n.position) < squaredCheckDist)
                    {
                        avoidanceDir += (Vector2)(lifeform.transform.position - n.position);
                    }
                }
                avoidanceDir /= lifeform.neighbors.SameFlock.Count;

                lifeform.NewMoveDir += avoidanceDir * weight;
            }
        }
    }
}