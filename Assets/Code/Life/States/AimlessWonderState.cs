using UnityEngine;
using System.Collections.Generic;

namespace AIAssignment3
{
    //The wonder state is made up of the following logic
    // - align with neighbors from the same flock
    // - avoid nearby obstacles
    // - keep a small distance from neighbors from the same flock so they don't overlap
    // - stay in radius of the gameplay area
    public class AimlessWonderState : StateBase
    {
        //Constructor
        public AimlessWonderState(Lifeform lifeform)
        {
            behaviors.Add(new BasicForwardMove(lifeform));
            behaviors.Add(new FlockAlignmentBehavior(lifeform));
            behaviors.Add(new ObstacleAvoidanceBehavior(lifeform));
            behaviors.Add(new StayInRadiusBehavior(lifeform));
            behaviors.Add(new SameFlockAvoidanceBehavior(lifeform));
        }

    }
}