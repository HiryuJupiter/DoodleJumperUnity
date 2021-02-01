using UnityEngine;
using System.Collections;

public class Ghost : PoolableObject
{
    [SerializeField] float lifeTime = 0.1f;

    public override void Respawned(Vector3 position)
    {
        base.Respawned(position);
    }

    IEnumerator Start()
    {
        yield return new WaitForSeconds(lifeTime);
        ReturnToPool();
    }
}