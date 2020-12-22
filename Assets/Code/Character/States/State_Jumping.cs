using UnityEngine;
using System.Collections.Generic;


public class State_Jumping : PlayerStateBase
{
    CharacterSettings settings;
    InputManager input;

    public State_Jumping(PlayerController player, PlayerFeedbacks feedbacks) : base(player, feedbacks)
    {}

    public override void StateEntry()
    {
        CharacterSettings settings = CharacterSettings.Instance;
        input = InputManager.Instance;
        Jump();
    } 

    public override void TickUpdate()
    {
        HorizontalInputUpdate();
    }

    void ApplyGravity()
    {
        if (player.velocity.y < settings.MaxFallSpeed)
        {
            player.velocity.y -= settings.Gravity * Time.deltaTime;
        }
    }

    void HorizontalInputUpdate()
    {
        player.velocity.x = Mathf.Lerp(player.velocity.x, input.MoveX * settings.MoveSpeed, settings.MoveAccceleration * Time.deltaTime);

        if (input.MoveX != 0)
        {
            feedback.SetFacing(input.MoveX > 0);
        }
    }

    void Jump() => player.velocity.y = settings.NormalJumpForce;
}
