using UnityEngine;
using System;
using System.Collections.Generic;
using System.Collections;

////When player is being propelled by a more powerful platform, do not let it get interrupted by a lower order platform.
//enum PlatformHierarchy
//{
//    Normal,
//    ExtraHigh,
//    Star,
//    Rainbow
//}

public class State_Jumping : PlayerStateBase
{
    bool canSteer = true;

    public State_Jumping(PlayerController player, PlayerFeedbacks feedbacks) : base(player, feedbacks) { }

    public override void StateEntry()
    {
        //ApplyJumpForce();
        canSteer = true;
        feedback.SetCharacterVisibility(true);
        float screenHalfWidth = OrthographicCameraUtil.GetScreenWidth / 2f;
    }

    public override void TickUpdate()
    {
        if (canSteer)
        {
            HorizontalInputUpdate();
            ScreenWarp();
        }
    }

    public override void TickFixedUpdate()
    {
        ApplyGravity();
    }

    public override void HitsInteractable(Interactable other)
    {
        if (TryPolymorphToTargetClass<JumpPlatform>(other, out JumpPlatform platform))
        {
            switch (platform.PlatformType)
            {
                case PlatformTypes.Basic:       HitsPlatform_Basic(platform);       break;
                case PlatformTypes.ExtraHigh:   HitsPlatform_ExtraHigh(platform);   break;
                case PlatformTypes.StarZoom:    HitsPlatform_StarZoom(platform);    break;
                case PlatformTypes.Rainbow:     HitsPlatform_Rainbow(platform);     break;
                case PlatformTypes.SingleJump:  HitsPlatform_SingleJump(platform);  break;
                case PlatformTypes.SideBumper:  HitsPlatform_SideBumper(platform);  break;
            }
        }
    }

    #region Character control
    void ApplyGravity()
    {
        if (playerStatus.Velocity.y > settings.MaxFallSpeed)
        {
            playerStatus.Velocity.y -= settings.Gravity * Time.deltaTime;
        }
    }

    void HorizontalInputUpdate()
    {
        //Horizontal movement
        playerStatus.Velocity.x = Mathf.Lerp(playerStatus.Velocity.x, input.MoveX * settings.MoveSpeed, settings.MoveAccceleration * Time.deltaTime);

        //Set facing
        if (input.MoveX != 0)
        {
            feedback.SetFacing(input.MoveX > 0);
        }
    }
    #endregion

    #region Object interaction
    void HitsPlatform_Basic(Interactable target)
    {
        if (IsVelocityBelow(settings.JumpForceNormal))
        {
            ApplyJumpForce(settings.JumpForceNormal);
        }
    }

    void HitsPlatform_ExtraHigh(Interactable target)
    {
        if (IsVelocityBelow(settings.JumpForceNormal))
        {
            Debug.Log("HitsPlatform_ExtraHigh");
            ApplyJumpForce(settings.JumpForceStrong);
            feedback.EnterMode_ExtraHigh();
        }
    }

    void HitsPlatform_StarZoom(Interactable target)
    {
        if (IsVelocityBelow(settings.JumpForceNormal))
        {
            Debug.Log("HitsPlatform_StarZoom");
            ApplyJumpForce(settings.JumpForceStrong, settings.JumpDuration_StarZoom);
            feedback.EnterMode_StarZoom();
        }
    }

    void HitsPlatform_Rainbow(Interactable target)
    {
        Debug.Log("HitsPlatform_Rainbow");
        ApplyJumpForce(settings.JumpForceStrong, settings.JumpDuration_Rainbow);
        feedback.EnterMode_Rainbow();
    }

    void HitsPlatform_SingleJump(Interactable target)
    {
        if (IsVelocityBelow(settings.JumpForceNormal))
        {
            Debug.Log("HitsPlatform_SingleJump");
            ApplyJumpForce(settings.JumpForceNormal);
            target.ReturnToPool();
        }
    }

    void HitsPlatform_SideBumper(Interactable target)
    {
        if (IsVelocityBelow(settings.JumpForceStrong))
        {
            Debug.Log("HitsPlatform_SideBumper");
            Vector2 force = settings.BumperForce;
            force.x = target.transform.position.x < 0 ? force.x : -force.x;
            ApplyJumpForce(force);

            target.ReturnToPool();
        }
    }
    #endregion

    #region Minor methods
    void ApplyJumpForce(float force, float duration = 0f)
    {
        player.StartCoroutine(DoApplyJumpForce(new Vector2(0f, force), duration));
    }

    void ApplyJumpForce(Vector2 force, float duration = 0f)
    {
        player.StartCoroutine(DoApplyJumpForce(force, duration));
    }

    IEnumerator DoApplyJumpForce(Vector2 force, float duration = 0f)
    {
        //Pause 1 frame so that the character is moved flush to the platform surface before applying force.
        playerStatus.Velocity = Vector2.zero;
        yield return null;

        do
        {
            playerStatus.Velocity = force;
            duration -= Time.deltaTime;
            yield return null;

            Debug.Log(" applying force..");
        }
        while (duration > 0f);
    }

    void ScreenWarp()
    {
        if (PastLeftEdge())
        {
            WarpToRightSide();
        }
        else if (PastRightEdge())
        {
            WarpToLeftSide();
        }
    }

    bool PastLeftEdge() => playerStatus.Velocity.x < -0.1f && player.transform.position.x < CameraTracker.LeftOfScreen;
    bool PastRightEdge() => playerStatus.Velocity.x > 0.1f && player.transform.position.x > CameraTracker.RightOfScreen;
    void WarpToLeftSide() => MovePlayerX(-CameraTracker.ScreenWidth - 0.1f);
    void WarpToRightSide() => MovePlayerX(CameraTracker.ScreenWidth + 0.1f);
    bool IsVelocityBelow(float speed) => playerStatus.Velocity.y < speed;

    void MovePlayerX(float amount)
    {
        Vector3 p = player.transform.position;
        p.x += amount;
        player.transform.position = p;
    }
    #endregion
}