using System.Collections.Generic;
using UnityEngine;

namespace AIAssignment3
{
    public class Flock : MonoBehaviour
    {
        [Header("Stats")]
        [SerializeField] float neighborRadius = 1.5f;
        [SerializeField] float mediumRadius = 1.0f;
        [SerializeField] float smallRadius = 0.5f;
        [SerializeField] float moveSpeed = 2f;
        [Range(0.01f, 0.3f)]
        [SerializeField] float steering = 0.2f; //1.0 for instant, 0.1 for smooth
        [SerializeField] LifeformTypes lifeformTypes;

        [Header("Prefab")]
        public Lifeform Prefab;

        List<Lifeform> inactive = new List<Lifeform>();
        List<Lifeform> active = new List<Lifeform>();

        //Properties
        public float NeighborRadius => neighborRadius;
        public float MoveSpeed => moveSpeed;
        public float MediumRadius => mediumRadius;
        public float SmallRadius => smallRadius;
        public float Steering => steering;
        public LifeformTypes LifeformType => lifeformTypes;

        void MassPopulate(int spawnAmount, float density = 0.08f)
        {
            //Spawn many lifeformes at the same time by placing them inside a circular area randomly
            float radius = spawnAmount * density;
            for (int i = 0; i < spawnAmount; i++)
            {
                Lifeform lifeform = Spawn(Random.insideUnitCircle * radius);
                lifeform.Initialize(this);
                active.Add(lifeform);
            }
        }

        //Spawn one lifeform at a time
        public Lifeform Spawn(Vector2 position)
        {
            Lifeform lifeform;
            //Try to pop an object from the object pool. If there isn't any inside the pool, 
            //...then instantiate one
            if (inactive.Count == 0)
            {
                lifeform = Instantiate(Prefab, position, GetRandomRotation(), transform);
                lifeform.Initialize(this);
            }
            else
            {
                //Pop from pool
                lifeform = inactive[0];
                inactive[0].transform.position = position;
                inactive[0].gameObject.SetActive(true);
                inactive.RemoveAt(0);
            }
            return lifeform;
        }

        //When despawning a lifeform, place it into the inactive pool 
        public void Despawn(Lifeform lifeform)
        {
            inactive.Add(lifeform);
            active.Remove(lifeform);
            lifeform.gameObject.SetActive(false);
        }

        //Get a random rotation 
        Quaternion GetRandomRotation() => Quaternion.Euler(new Vector3(0f, 0f, Random.Range(0, 360f)));
    }

}