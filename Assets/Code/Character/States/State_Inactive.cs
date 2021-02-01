using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class State_Inactive : PlayerStateBase
{
    public State_Inactive(PlayerController player, PlayerFeedbacks feedback) : base(player, feedback)
    { }

    public override void StateEntry()
    {
        feedback.SetCharacterVisibility(false);
        playerStatus.Velocity = Vector2.zero;
        //if (player.velocity.y > 0)
        //{
        //    player.velocity.y = 0;
        //}
    }

    public override void StateExit()
    {
        feedback.SetCharacterVisibility(true);

        //Place character in center of the screen

        //Apply a jumping force

        //Spawn a default chicken at the bottom
    }

    public override void TickUpdate()
    { }
}

