namespace AIAssignment3
{
    //The waypoint state is made up of the following logic
    // - align with neighbors from the same flock
    // - avoid nearby obstacles
    // - keep a small distance from neighbors from the same flock so they don't overlap
    // - Follow waypoints
    public class WaypointPatrolState : StateBase
    {
        //Constructor
        public WaypointPatrolState(Lifeform lifeform)
        {
            behaviors.Add(new FlockAlignmentBehavior(lifeform));
            behaviors.Add(new ObstacleAvoidanceBehavior(lifeform));
            behaviors.Add(new SameFlockAvoidanceBehavior(lifeform));
            behaviors.Add(new WaypointFollowBehavior(lifeform));
        }
    }
}