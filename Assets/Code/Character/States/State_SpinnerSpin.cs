using UnityEngine;
using System.Collections.Generic;

public class State_SpinnerSpin : PlayerStateBase
{
    const float RotationSpeed = 2f;

    
    //Status
    Spinner spinner;
    int facingModifier = 1;
    Vector2 dirToCenter;

    public State_SpinnerSpin(PlayerController player, PlayerFeedbacks feedbacks) : base(player, feedbacks)
    {
    }

    public override void StateEntry() 
    {
        playerStatus.Velocity = Vector2.zero;

        UpdateDirectionToCenter();
        FindFacing();
        SetOrbitPosition();
        SetOrbitRotation();
    } 

    public override void TickUpdate()
    {
        RotatePlayer();
        if (input.PressedMoveKey)
        {
            ReleasePlayer();
        }
    }

    public override void HitsInteractable(Interactable other) 
    {
        if (TryPolymorphToTargetClass<Spinner>(other, out Spinner spinner))
        {
            //Reference and initialize
            this.spinner = spinner;

            //Orbit calculations
            spinner.CollidedWithPlayer(player);
        }
    }

    void ReleasePlayer ()
    {
        playerStatus.SetRecentlyHitSpinner(spinner);
    }

    void RotatePlayer ()
    {
        dirToCenter = Quaternion.Euler(0f, 0f, RotationSpeed * facingModifier) * dirToCenter;
        SetOrbitPosition();
        SetOrbitRotation();
    }

    #region Minor methods
    void UpdateDirectionToCenter()
    {
        dirToCenter = (spinner.transform.position - player.transform.position).normalized;
    }

    void SetOrbitRotation()
    {
        player.transform.rotation = Quaternion.LookRotation(Vector3.forward, dirToCenter) * Quaternion.Euler(0f, 0f, facingModifier * -90f);
    }

    void SetOrbitPosition()
    {
        player.transform.position = (Vector2)spinner.transform.position + (-dirToCenter * spinner.Radius);
    }

    void FindFacing()
    {
        Vector3 cross = Vector3.Cross(dirToCenter, player.transform.up);
        facingModifier = cross.z > 0f ? -1 : 1;
    }
    #endregion
}

/*
 using UnityEngine;
using System.Collections.Generic;

public class State_SpinnerSpin : PlayerStateBase
{
    const float RotationSpeed = 2f;

    //Reference
    InputManager input;
    
    //Status
    Spinner spinner;
    float currentAngle;
    int facingModifier;

    public State_SpinnerSpin(PlayerController player, PlayerFeedbacks feedbacks) : base(player, feedbacks)
    {
        input = InputManager.Instance;
    }

    public override void StateEntry() 
    {
    
    } 

    public override void TickUpdate()
    {
        RotatePlayer();
        if (input.PressedMoveKey)
        {
            ReleasePlayer();
        }
    }

    public override void HitsInteractable(Interactable other) 
    {
        if (TryPolymorphToTargetClass<Spinner>(other, out Spinner spinner))
        {
            //Reference and initialize
            this.spinner = spinner;
            playerStatus.Velocity = Vector2.zero;

            //Orbit calculations
            spinner.CollidedWithPlayer(player);
            FindPlayerOrbitAngleAndFacing();
        }
    }

    void ReleasePlayer ()
    {
        playerStatus.SetRecentlyHitSpinner(spinner);
    }

    void RotatePlayer ()
    {
        currentAngle += RotationSpeed * facingModifier * Time.deltaTime;
        player.transform.localRotation = Quaternion.Euler(0f, 0f, currentAngle);
    }

    void FindPlayerOrbitAngleAndFacing ()
    {
        Vector2 dirToSpinner = spinner.transform.position - player.transform.position;
        currentAngle = Vector2.Angle(Vector3.right, dirToSpinner);
        //Vector3.Cross(dirToSpinner, player.transform.up);
    }
}
 */