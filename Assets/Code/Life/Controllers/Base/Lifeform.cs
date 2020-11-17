using UnityEngine;

namespace AIAssignment3
{
    //Require component to minimize the mistake of forgetting to add it
    [RequireComponent(typeof(Collider2D))]
    //Use an abstract class to reuse code
    public abstract class Lifeform : MonoBehaviour
    {
        //Status
        [HideInInspector]
        public Vector2 NewMoveDir;

        Vector2 oldMoveDir;

        //Stats
        float steering;

        //Cache
        Color startingColor;

        //Properties
        public Flock Flock { get; protected set; }
        public bool inHidingArea { get; protected set; }
        public bool inDanger { get; protected set; }
        public Collider2D Collider { get; protected set; }
        public SpriteRenderer Renderer { get; protected set; }
        public LifeformTypes LifeFormType { get; protected set; }
        public Neighbors neighbors { get; protected set; }
        protected float MoveSpeed => (inHidingArea && inDanger) ? 0.1f : Flock.MoveSpeed;

        #region Public
        //Use an Initialize method to tell this lifeform which flock it belongs to
        public virtual void Initialize(Flock flock)
        {
           
            //Reference
            Collider = GetComponent<Collider2D>();
            Renderer = GetComponent<SpriteRenderer>();

            //Cache
            Flock = flock;
            LifeFormType = flock.LifeformType;
            steering = flock.Steering;
            startingColor = Renderer.color;

            //Initialize 
            neighbors = new Neighbors(this);

            //Debug.Log("spawned lifeform of type :" + lifeformType +   ", number: " + (int)lifeformType);
        }

        public void ExecuteMovement()
        {
            //Execute movement by modifying transform.position
            NewMoveDir = Vector2.Lerp(oldMoveDir, NewMoveDir.normalized * MoveSpeed, steering);
            transform.up = NewMoveDir;
            transform.position += (Vector3)NewMoveDir * Time.deltaTime;

            oldMoveDir = NewMoveDir;
        }

        public void GetsEaten()
        {
            //Tell the flock to despawn this lifeform when it is eaten
            Flock.Despawn(this);
        }

        public void ResetColor ()
        {
            Renderer.color = startingColor;
        }
        #endregion
    }
}