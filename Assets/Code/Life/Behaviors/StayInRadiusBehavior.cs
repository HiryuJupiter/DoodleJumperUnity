using UnityEngine;

namespace AIAssignment3
{
    public class StayInRadiusBehavior : BehaviorBase
    {
        Vector2 center = Vector2.zero;
        const float radius = 8f;
        const float radiusSqr = radius * radius;

        public StayInRadiusBehavior(Lifeform lifeform) : base(lifeform) {}

        public override void TickFixedUpdate()
        {
            //If the lifeform is at the outer edge of the radius, make it move towards center
            Vector2 dirToCenter = center - (Vector2)transform.position;
            float percent = dirToCenter.sqrMagnitude / radiusSqr;

            if (percent > 0.7f)
            {
                //Make the attraction force stronger the further away it is.
                lifeform.NewMoveDir += dirToCenter * percent * percent;
            }
        }
    }
}