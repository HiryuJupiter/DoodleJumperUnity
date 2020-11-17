using UnityEngine;
using System.Collections.Generic;

namespace AIAssignment3
{
    public abstract class StateBase
    {
        //Variables
        protected List<BehaviorBase> behaviors = new List<BehaviorBase>();

        //Override fixed update
        public virtual void StateFixedUpdate()
        {
            foreach (var b in behaviors)
            {
                b.TickFixedUpdate();
            }
        }
    }
}