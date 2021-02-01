using UnityEngine;
using System.Collections;

public class LinearTranslator : Interactable
{
    [SerializeField] float translateSpeed;
    [SerializeField] Transform startingPoint;
    [SerializeField] Transform endPoint;

    public float Speed => translateSpeed;
    public Vector2 Direction { get; private set; }
    
    void Awake()
    {
        InteractableType = InteractableTypes.LinearTranslator;
        Direction = (endPoint.position - startingPoint.position).normalized;
    }

    public override void CollidedWithPlayer(PlayerController player)
    {
        //AlignPlayerOnTrack(player);
    }

    #region AlignToTrack
    void AlignPlayerOnTrack(PlayerController player)
    {
        //Place player on track based on the player's current distance to endpoint
        float distToEndpoint = DistanceToEndPoint(player.transform.position);
        Vector2 positionAlongTheTrack = PositionFacingEndpointByDistance(distToEndpoint);
        player.transform.position = positionAlongTheTrack;
    }

    float DistanceToEndPoint(Vector2 position) => Vector2.Distance(position, endPoint.position);
    Vector2 PositionFacingEndpointByDistance(float distanceToEndpoint) => (Vector2)endPoint.position - Direction * distanceToEndpoint;
    #endregion
}