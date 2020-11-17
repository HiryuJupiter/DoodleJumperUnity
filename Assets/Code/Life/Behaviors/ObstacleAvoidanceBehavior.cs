using UnityEngine;

namespace AIAssignment3
{
    public class ObstacleAvoidanceBehavior : BehaviorBase
    {
        const float weight = 2f;
        float squaredCheckDist;

        //Constructor
        public ObstacleAvoidanceBehavior(Lifeform lifeform) : base(lifeform)
        {
            squaredCheckDist = flock.SmallRadius * flock.SmallRadius;
        }

        public override void TickFixedUpdate()
        {
            //Find the direction that allows us to avoid nearby obstacles
            if (lifeform.neighbors.Obstacles.Count > 0)
            {
                Vector2 avoidanceDir = Vector2.zero;

                foreach (Transform n in lifeform.neighbors.Obstacles)
                {
                    //Only consider the obstacles that are very close to us
                    if (Vector2.SqrMagnitude(transform.position - n.position) < squaredCheckDist)
                    {
                        avoidanceDir += (Vector2)(transform.position - n.position);
                    }
                }

                //Average out the move direction
                avoidanceDir /= lifeform.neighbors.Obstacles.Count;
                lifeform.NewMoveDir += avoidanceDir * weight;
            }
        }
    }
}