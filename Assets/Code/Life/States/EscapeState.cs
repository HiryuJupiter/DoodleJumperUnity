namespace AIAssignment3
{
    //The escape state is composed of the following logic: staying 
    //in radius and escaping from predator by hiding in a nearby hide spot 
    //    or running away from the predator if no hide spot is available
    public class EscapeState : StateBase
    {
        //Constructor
        public EscapeState(Lifeform lifeform)
        {
            behaviors.Add(new EscapeMovementBehavior(lifeform));
            behaviors.Add(new StayInRadiusBehavior(lifeform));
        }
    }
}