using UnityEngine;
using System.Collections.Generic;
using System.Collections;



public class State_Inactive : PlayerStateBase
{
    public State_Inactive(PlayerController player, PlayerFeedbacks feedback) : base(player, feedback)
    { }

    public override void StateEntry()
    { }

    public override void TickUpdate()
    { }
}

