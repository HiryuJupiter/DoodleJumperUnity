using UnityEngine;
using System.Collections.Generic;

public class State_LinearTranslate : PlayerStateBase
{
    const float TranslationDuration = 0.2f;

    //Status
    LinearTranslator translator;
    float timeRemaining;

    public State_LinearTranslate(PlayerController player, PlayerFeedbacks feedbacks) : base(player, feedbacks)
    {}

    public override void StateEntry() 
    {
        translator = null;
    }

    public override void TickUpdate()
    {
        TranslatePlayer();
        TickTimer();
    }

    public override void HitsInteractable(Interactable other)
    {
        if (TryPolymorphToTargetClass<LinearTranslator>(other, out LinearTranslator translator))
        {
            RefreshTimeRemaining();
            this.translator = translator;
            //translator.CollidedWithPlayer(player);
        }
    }

    void TranslatePlayer ()
    {
        playerStatus.Velocity = translator.Direction * translator.Speed * Time.deltaTime;
    }

    #region Timer
    void RefreshTimeRemaining() => timeRemaining = TranslationDuration;

    void TickTimer ()
    {
        timeRemaining -= Time.deltaTime;
        if (timeRemaining <= 0)
        {
            player.SwitchToNewState(PlayerStates.Jumping);
        }
    }
    #endregion
}

/*
 Hitting a translator
Find the closest waypoint and move towards that (so that the player is translating at the middle of the belt)
then movetowards the next waypoint


    public override void CollidedWithPlayer(PlayerController player)
    {
        AlignPlayerOnTrack(player);
    }

    #region AlignToTrack
    void AlignPlayerOnTrack(PlayerController player)
    {
        //Place player on track based on the player's current distance to endpoint
        float distToEndpoint = DistanceToEndPoint(player.transform.position);
        Vector2 positionAlongTheTrack = PositionFacingEndpointByDistance(distToEndpoint);
        player.transform.position = positionAlongTheTrack;
    }

    float DistanceToEndPoint(Vector2 position) => Vector2.Distance(position, endPoint);
    Vector2 PositionFacingEndpointByDistance(float distanceToEndpoint) => endPoint - Direction * distanceToEndpoint;
    #endregion

 */