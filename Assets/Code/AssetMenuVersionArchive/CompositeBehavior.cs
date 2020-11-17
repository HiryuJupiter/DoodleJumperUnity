using System.Collections.Generic;
using UnityEngine;

namespace AIAssignment3
{
    //Create a menu item that can be opened using RMB in the editor, that allows the player to create a scriptable object
    [CreateAssetMenu(menuName = "Flock/Behavior/Composite")]
    public class CompositeBehavior : FlockBehavior
    {
        //Internal struct 
        [System.Serializable]
        public struct BehaviourGroup
        {
            public FlockBehavior behaviours;
            public float weights;
        }

        //Variables
        //This list of behaviors is the key to the composite pattern
        public BehaviourGroup[] behaviours;

        public override Vector2 CalculateMove(FlockAgent agent, List<Transform> context, Flock flock)
        {
            Vector2 move = Vector2.zero;

            //Run through each of the behaviors to calculate movement and then aggregate the result on the move variable
            for (int i = 0; i < behaviours.Length; i++)
            {
                Vector2 partialMove = behaviours[i].behaviours.CalculateMove(agent, context, flock) * behaviours[i].weights;
                if (partialMove != Vector2.zero)
                {
                    if (partialMove.sqrMagnitude > behaviours[i].weights * behaviours[i].weights)
                    {
                        partialMove.Normalize();
                        partialMove *= behaviours[i].weights;
                    }
                    move += partialMove;
                }
            }
            return move;
        }
    }
}