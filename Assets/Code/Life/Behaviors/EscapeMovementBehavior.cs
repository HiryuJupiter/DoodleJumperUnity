using UnityEngine;

namespace AIAssignment3
{
    public class EscapeMovementBehavior : BehaviorBase
    {
        //Using consts for stats that we want to use
        const float HideWeight = 5f;

        //Cache
        protected float sqrNeighborRadius;

        //Constructor
        public EscapeMovementBehavior(Lifeform lifeform) : base (lifeform)
        {
            sqrNeighborRadius = flock.NeighborRadius * flock.NeighborRadius;
        }

        public override void TickFixedUpdate()
        {
            if (lifeform.neighbors.HasHideSpot())
            {
                Vector2 hideDir = lifeform.neighbors.GetClosestHideSpot().position - transform.position;
                lifeform.NewMoveDir += hideDir.normalized * HideWeight;
            }
            else
            {
                //If there is no hiding spot, then run away
                Vector3 escapeDir = (transform.position - lifeform.neighbors.GetClosestPredator().position);
                Debug.DrawRay(transform.position, escapeDir, Color.cyan);
                //Escape harder if the enemy is closer
                //float perc = 1 - escapeDir.sqrMagnitude / sqrNeighborRadius;
                lifeform.NewMoveDir += (Vector2)escapeDir;
            }
        }
    }
}

/*
 public void TickUpdate(Lifeform lifeform, Neighbors neighbors, Flock flock)
        {
            Vector2 escapeDir = Vector2.zero;

            if (neighbors.HasHideSpot())
            {
                Vector2 dir = neighbors.GetClosestHideSpot().position - lifeform.transform.position;

                //Hide logic version 1: hide inside the hide zones
                if (Vector2.SqrMagnitude(dir) > flock.SmallRadius)
                {
                    //Debug.DrawRay(lifeform.transform.position, escapeDir, Color.yellow);
                    escapeDir = dir.normalized * HideWeight;
                }

                //Hide logic version 2: hide behind objects
                escapeDir = (Vector2)neighbors.GetClosestHideSpot().position + dir.normalized * HideBehindObjectDist;
                escapeDir = dir * HideWeight;
                Debug.DrawLine(lifeform.transform.position, neighbors.GetClosestHideSpot().position, Color.green);
            }
            else
            {
                //If there is no hiding spot, then run away
                //We use normalized to make the movement less powerful
                escapeDir = lifeform.transform.position - neighbors.GetClosestPredator().position;

                //Escape harder if the enemy is closer
                float perc = sqrNeighborRadius - escapeDir.sqrMagnitude / sqrNeighborRadius;
                escapeDir = escapeDir * perc;
                //Debug.DrawRay(lifeform.transform.position, escapeDir, Color.blue);
            }

            return escapeDir;
        }
 */