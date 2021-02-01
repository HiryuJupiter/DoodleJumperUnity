using UnityEngine;
using System.Collections;

public abstract class Interactable : PoolableObject
{
    public InteractableTypes InteractableType { get; protected set; }

    public virtual void CollidedWithPlayer(PlayerController player) { }

    protected virtual void Update()
    {
        CheckIfOffscreen();
    }

    protected void CheckIfOffscreen ()
    {
        if (transform.position.y < CameraTracker.BottomOfScreen)
        {
            ReturnToPool();
        }
    }
}