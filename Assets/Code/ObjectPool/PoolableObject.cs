using UnityEngine;
using System.Collections;

public abstract class PoolableObject : MonoBehaviour
{
    protected ObjectPool pool;

    public virtual void Initialize_SetPool (ObjectPool pool)
    {
        this.pool = pool;
    }

    public virtual void Despawn ()
    {
        ReturnToPool();
    }

    public virtual void Respawned(Vector3 position) 
    {
        transform.position = position;
    }

    //public virtual void Respawned(Vector3 pos) 
    //{
    //    transform.position = pos;
    //    Respawned();
    //}

    public virtual void ReturnToPool()
    {
        pool.DirectDespawn(this);
    }
}