using UnityEngine;

namespace AIAssignment3
{
    //Tell Unity editor that we need a 
    [RequireComponent(typeof(Collider2D))]
    public class FlockAgent : MonoBehaviour
    {
        //Reference
        Flock flock;
        Collider2D collider;

        //Property
        public Flock Flock => flock;
        public Collider2D AgentCollider => collider;

        void Awake()
        {
            //Reference component
            collider = GetComponent<Collider2D>();
        }

        public void Initialize(Flock flock)
        {
            //Cache the reference
            this.flock = flock;
        }

        public void Move(Vector2 velocity)
        {
            //Execute the physics movement
            transform.up = velocity;
            transform.position += (Vector3)velocity * Time.deltaTime;
        }
    }
}