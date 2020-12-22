using UnityEngine;
using System.Collections.Generic;

public abstract class PlayerStateBase
{
    protected CharacterSettings settings;
    protected PlayerController player;
    protected PlayerFeedbacks feedback;

    public PlayerStateBase(PlayerController player, PlayerFeedbacks feedback)
    {
        this.player = player;
        this.feedback = feedback;
        settings = CharacterSettings.Instance;
    }

    public virtual void StateEntry() {}
    public virtual void StateExit() {}
    public virtual void TickUpdate() {}
    public virtual void PlayerTakesDamage() { }
    public virtual void TickFixedUpdate() { }
}
