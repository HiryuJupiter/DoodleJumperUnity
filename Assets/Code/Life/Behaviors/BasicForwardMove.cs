using UnityEngine;

namespace AIAssignment3
{
    public class BasicForwardMove : BehaviorBase
    {
        //Constructor
        public BasicForwardMove(Lifeform lifeform) : base(lifeform) {}

        public override void TickFixedUpdate()
        {
            lifeform.NewMoveDir += (Vector2)transform.up;
        }
    }
}