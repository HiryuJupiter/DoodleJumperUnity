using UnityEngine;
using System.Collections;

public abstract class PoolObject : MonoBehaviour
{
    protected Pool pool;
    protected CameraTracker cam;

    public void Initialize_SetPool (Pool pool)
    {
        this.pool = pool;
        cam = CameraTracker.Instance;
    }

    protected void ReturnToPool()
    {
        pool.Despawn(gameObject);
    }
    public virtual void Respawned() { }
    public virtual void Respawned(Vector3 pos) 
    {
        transform.position = pos;
        Respawned();
    }
}