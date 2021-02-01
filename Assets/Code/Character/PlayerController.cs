using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(RigidbodyVerticalRaycaster))]
public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance;

    //Reference
    Rigidbody2D rb;
    GameManager gm;
    GameSettings settings;
    PlayerFeedbacks feedback;
    RigidbodyVerticalRaycaster raycaster;

    //States machine
    Dictionary<PlayerStates, PlayerStateBase> stateClassLookup;
    PlayerStates currentStateType;
    PlayerStateBase currentStateClass;

    //Cache
    Vector2 startingPosition;

    //Properties
    public PlayerStatus Status { get; private set; } = new PlayerStatus();
    bool IsPlayerActive => currentStateType != PlayerStates.Inactive;

    #region MonoBehavior
    private void Awake()
    {
        //Lazy singleton
        Instance = this;

        //Reference
        rb = GetComponent<Rigidbody2D>();
        feedback = GetComponentInChildren<PlayerFeedbacks>();
        raycaster = GetComponent<RigidbodyVerticalRaycaster>();

        //Initialize
        stateClassLookup = new Dictionary<PlayerStates, PlayerStateBase>
        {
            {PlayerStates.Inactive,         new State_Inactive          (this, feedback)},
            {PlayerStates.Jumping,          new State_Jumping           (this, feedback)},
            {PlayerStates.LinearTranslate,  new State_LinearTranslate   (this, feedback)},
            {PlayerStates.Spinning,         new State_SpinnerSpin       (this, feedback)},
        };

        currentStateType = PlayerStates.Inactive;
        currentStateClass = stateClassLookup[currentStateType];
        currentStateClass.StateEntry();

        feedback.SetCharacterVisibility(false);

        //Cache
        startingPosition = transform.position;
    }

    void Start()
    {
        //Reference
        gm = GameManager.Instance;
        settings = GameSettings.Instance;

        GameManager.OnGameStart += GameStarts;
        GameManager.OnGameOver += GameOver;
    }

    void OnDisable()
    {
        GameManager.OnGameStart -= GameStarts;
        GameManager.OnGameOver -= GameOver;
    }

    void Update()
    {
        currentStateClass?.TickUpdate();
    }

    void FixedUpdate()
    {
        currentStateClass?.TickFixedUpdate();

        if (IsPlayerActive)
        {
            Status.TimerUpdate();
            raycaster.CheckForCollidersBeneath(RaycastFoundColliderBelow);

            CheckIfFallenToDeath();

            ExecuteMovement();
        }
    }

    void OnTriggerStay2D(Collider2D col)
    {
        if (IsColliderInteractableObject(col))
        {
            Interactable other = col.GetComponent<Interactable>();
            InteractableTypes type = other.InteractableType;
            switch (type)
            {
                case InteractableTypes.Platform:
                    HitsPlatform(other);
                    break;
                case InteractableTypes.LinearTranslator:
                    HitsLinearTranslator(other);
                    break;
                case InteractableTypes.Spinner:
                    HitsSpinner(other);
                    break;
                case InteractableTypes.Gem:
                    HitsGem(other);
                    break;
            }
        }
    }

    void OnGUI()
    {
        GUI.Label(new Rect(20, 50, 500, 50), "Player state : " + currentStateType, GameSettings.GuiStyle);
        GUI.Label(new Rect(20, 100, 500, 50), "rb.velocity : " + rb.velocity, GameSettings.GuiStyle);
        GUI.Label(new Rect(20, 150, 500, 50), "position : " + transform.position, GameSettings.GuiStyle);
    }
    #endregion

    #region Public
    public void SwitchToNewState(PlayerStates newState)
    {
        if (currentStateType != newState)
        {
            currentStateType = newState;

            currentStateClass.StateExit();
            currentStateClass = stateClassLookup[newState];
            currentStateClass.StateEntry();
        }
    }
    #endregion

    #region Collision with interactables
    void RaycastFoundColliderBelow(Collider2D col)
    {
        if (IsColliderInteractableObject(col))
        {
            Interactable other = col.GetComponent<Interactable>();
            if (other.InteractableType == InteractableTypes.Platform)
            {
                HitsPlatform(other);
            }
        }
    }

    void HitsPlatform(Interactable platform)
    {
        if (!Status.IsPlatformRecentlyHit(platform))
        {
            Status.SetRecentlyHitPlatform(platform);
            SwitchToNewState(PlayerStates.Jumping);
            currentStateClass?.HitsInteractable(platform);
        }
    }

    void HitsLinearTranslator(Interactable translator)
    {
        SwitchToNewState(PlayerStates.LinearTranslate);
        currentStateClass?.HitsInteractable(translator);
    }

    void HitsSpinner(Interactable spinner)
    {
        if (!Status.IsSpinnerRecentlyHit(spinner))
        {
            Status.SetRecentlyHitSpinner(spinner);
            SwitchToNewState(PlayerStates.Spinning);
            currentStateClass?.HitsInteractable(spinner);
        }
    }

    void HitsGem(Interactable other)
    {

    }
    #endregion

    #region Minor Methods
    void GameStarts()
    {
        ResetPosition();
        SwitchToNewState(PlayerStates.Jumping);
    }

    void CheckIfFallenToDeath()
    {
        if (transform.position.y < CameraTracker.BottomOfScreen - 1f)
        {
            Debug.Log("Fallen to death. Player position " + transform.position + ", camera bottom = " + CameraTracker.BottomOfScreen);
            gm.PlayerDead();
        }
    }

    void ResetPosition() => transform.position = startingPosition;
    void GameOver() => SwitchToNewState(PlayerStates.Inactive);
    void ExecuteMovement() => rb.velocity = Status.Velocity;
    bool IsColliderInteractableObject(Collider2D collider) => settings.InteractableLayer == (settings.InteractableLayer | 1 << collider.gameObject.layer);
    #endregion
}