using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AIAssignment3
{
    public class ChaseAndAttackBehavior : BehaviorBase
    {
        float sqrAttackDist;

        //Constructor, cache the squared distance for calculations
        public ChaseAndAttackBehavior(Lifeform lifeform) : base(lifeform) 
        {
            sqrAttackDist = lifeform.Flock.SmallRadius * lifeform.Flock.SmallRadius / 2f;
        }

        public override void TickFixedUpdate()
        {
            Transform prey = GetClosestPrey();

            if (prey != null)
            {
                Vector3 direction = prey.position - transform.position;
                Debug.DrawRay(transform.position, direction, Color.red);

                //If the prey is close enough, then do attack. Otherwise chase
                if (Vector2.SqrMagnitude(direction) < sqrAttackDist)
                {
                    Attack();
                    lifeform.NewMoveDir = Vector2.zero;
                }
                else
                {
                    lifeform.NewMoveDir += (Vector2)(prey.position - transform.position).normalized;
                }
            }
        }

        void Attack ()
        {
            lifeform.StartCoroutine(DoAttack());
        }

        IEnumerator DoAttack ()
        {
            lifeform.Renderer.color = Color.red;
            yield return new WaitForSeconds(0.1f);
            lifeform.ResetColor();
        }

        Transform GetClosestPrey()
        {
            Transform prey = null;

            List<Lifeform> preys = lifeform.neighbors.Preys;

            if (preys.Count > 0)
            {
                //If there is only one prey, then skip trying to find the closest prey
                if (preys.Count == 1)
                {
                    prey = preys[0].transform;
                }
                else if (preys.Count > 1)
                {
                    //Find closest prey
                    prey = lifeform.neighbors.GetClosestPrey();
                }
            }
            return prey;
        }
    }
}