using UnityEngine;

namespace AIAssignment3
{
    public class SameFlockCohesion : BehaviorBase
    {
        //Stats
        const float Weight = 0.1f;

        float smoothTime = 0.1f;
        float squaredCheckDist;

        //Status
        Vector2 smoothDampVelocity;

        //Constructor
        public SameFlockCohesion(Lifeform lifeform) : base(lifeform)
        {
            squaredCheckDist = flock.MediumRadius * flock.MediumRadius;
        }

        public override void TickFixedUpdate()
        {
            if (lifeform.neighbors.SameFlock.Count > 0)
            {
                Vector2 move = Vector2.zero;

                //Find the averaged mid point of nearby neighbors
                foreach (Transform n in lifeform.neighbors.SameFlock)
                {
                    if (Vector2.SqrMagnitude(transform.position - n.position) < squaredCheckDist)
                    {
                        move += (Vector2)n.position;
                    }
                }
                move /= lifeform.neighbors.SameFlock.Count;

                //Move the lifeform towards that position
                move -= (Vector2)transform.position;
                move = Vector2.SmoothDamp(transform.up, move, ref smoothDampVelocity, smoothTime) * Weight;

                lifeform.NewMoveDir += move;
            }
        }
    }
}