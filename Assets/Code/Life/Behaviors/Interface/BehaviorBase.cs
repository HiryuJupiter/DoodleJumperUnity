using UnityEngine;

namespace AIAssignment3
{
    //Using an abstract class to specify the contents of 
    public abstract class BehaviorBase
    {
        protected Lifeform lifeform;
        protected Transform transform;
        protected Flock flock;

        public BehaviorBase(Lifeform lifeform)
        {
            this.lifeform = lifeform;
            transform = lifeform.transform;
            flock = lifeform.Flock;
        }

        public abstract void TickFixedUpdate();
    }
}