using UnityEngine;
using System.Collections;

public class Spinner : Interactable
{
    [SerializeField] float radius;

    public float Radius => radius;

    void Awake()
    {
        InteractableType = InteractableTypes.Spinner;
    }

    public override void CollidedWithPlayer(PlayerController player)
    {
        //MovePlayerToRadiusDistance(player.transform);
    }

    void MovePlayerToRadiusDistance (Transform player)
    {
        Vector2 playerPos = player.transform.position;
        Vector2 dirToCenter = (Vector2)transform.position - playerPos;
        Vector2 correctedDirection = dirToCenter.normalized * radius;
    }
}