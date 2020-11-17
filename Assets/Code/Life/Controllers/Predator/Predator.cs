using UnityEngine;

namespace AIAssignment3
{
    public class Predator : Lifeform
    {
        //State classes
        protected StateBase huntState;
        protected StateBase waypointMoveState;

        public override void Initialize(Flock flock)
        {
            base.Initialize(flock);
            huntState = new HuntState(this);
            waypointMoveState = new WaypointPatrolState(this);
        }

        //Override fixed update to skip the step of looking for prey
        protected void FixedUpdate()
        {
            NewMoveDir = Vector2.zero;

            neighbors.DetectNeighbors();
            if (neighbors.HasPrey())
            {
                huntState.StateFixedUpdate();
            }
            else
            {
                waypointMoveState.StateFixedUpdate();
            }

            ExecuteMovement();
        }
    }
}