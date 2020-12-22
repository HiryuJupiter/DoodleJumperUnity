using UnityEngine;
using System.Collections;

public class Interactable : PoolObject
{
    void Update()
    {
        if (transform.position.y < cam.BottomOfScreen)
        {
            ReturnToPool();
        }
    }

    public virtual void PlayerCollisionEffect(PlayerController player)
    {
        ReturnToPool();
    }
}