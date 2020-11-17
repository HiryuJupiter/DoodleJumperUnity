using UnityEngine;

namespace AIAssignment3
{
    public class Prey : Lifeform
    {
        //State classes
        protected StateBase escapeState;
        protected StateBase passiveState;

        //Cache
        int layerMask_Hide;

        //Override the base class Initialze method so we ...
        //...can specify which particular state classes to assign
        public override void Initialize(Flock flock)
        {
            base.Initialize(flock);

            layerMask_Hide = Settings.instance.Layer_HideSpot;

            escapeState = new EscapeState(this);
            passiveState = new AimlessWonderState(this);
        }

        //Override fixed update to skip the step of looking for prey
        protected void FixedUpdate()
        {
            NewMoveDir = Vector2.zero;

            neighbors.DetectNeighbors();

            inDanger = neighbors.HasPredator();
            if (inDanger)
            {
                if (inHidingArea)
                {
                    //Do nothing and remain still
                }
                else
                {
                    escapeState.StateFixedUpdate();
                }
            }
            else
            {
                passiveState.StateFixedUpdate();
            }

            ExecuteMovement();
        }

        void OnTriggerEnter2D(Collider2D collision)
        {
            //When the lifeform enters a hiding-zone, change the boolean to true
            if (IsThisColliderHideLayer(collision))
            {

                inHidingArea = true;
            }
        }

        void OnTriggerExit2D(Collider2D collision)
        {
            //When the lifeform exits a hiding-zone, change this boolean to false
            if (IsThisColliderHideLayer(collision))
            {
                inHidingArea = false;
            }
        }

        //See if the collided object is a hide layer
        bool IsThisColliderHideLayer(Collider2D collision)
        {
            return layerMask_Hide == (layerMask_Hide | 1 << collision.gameObject.layer);
        }
    }
}