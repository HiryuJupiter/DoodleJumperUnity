using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance;

    [HideInInspector] public Vector2 velocity = Vector2.zero;

   

    //Reference
    Rigidbody2D rb;
    CameraTracker cam;
    GameManager gameManager;
    CharacterSettings settings;
    PlayerFeedbacks feedback;

    //Status
    Dictionary<PlayerStates, PlayerStateBase> stateClassLookup;
    PlayerStates currentStateType;
    PlayerStateBase currentStateClass;

    //Properties
    bool Falling => velocity.y < 0;

    #region MonoBehavior
    void Awake()
    {
        //Lazy singleton
        Instance = this;

        //Reference
        rb          = GetComponent<Rigidbody2D>();
        feedback    = GetComponentInChildren<PlayerFeedbacks>();

        //Initialize
        stateClassLookup = new Dictionary<PlayerStates, PlayerStateBase>
        {
            {PlayerStates.Inactive,     new State_Inactive      (this, feedback)},
            {PlayerStates.Jumping,      new State_Jumping       (this, feedback)},
            {PlayerStates.Slamming,     new State_Slamming      (this, feedback)},
            {PlayerStates.Dashing,      new State_Dashing       (this, feedback)},
            {PlayerStates.Rocketing,    new State_Rocketing     (this, feedback)},
            {PlayerStates.Balooning,    new State_Ballooning    (this, feedback)},
        };

        currentStateType    = PlayerStates.Inactive;
        currentStateClass   = stateClassLookup[currentStateType];
    }

    void Start()
    {
        //Reference
        cam             = CameraTracker.Instance;
        gameManager     = GameManager.Instance;
        settings        = CharacterSettings.Instance;

        EventSubscribing();
    }

    void Update()
    {
        currentStateClass?.TickUpdate();
    }

    void FixedUpdate()
    {
        currentStateClass?.TickFixedUpdate();

        if (currentStateType != PlayerStates.Inactive)
        {
            CheckIfFallenToDeath();
            ExecuteMovement();
        }
    }

    void OnTriggerStay2D(Collider2D collider)
    {
        if (Falling && HitsInteractableObject(collider))
        {
            feedback.Play_LandsOnPlatform();
            collider.GetComponent<Interactable>().PlayerCollisionEffect(this);
        }
    }

    void OnDisable() => EventUnsubscribing();
    #endregion

    #region Gameplay events
    void CheckIfFallenToDeath()
    {
        if (transform.position.y < cam.BottomOfScreen)
        {
            SceneEvents.PlayerDead.InvokeEvent();
        }
    }
    #endregion

    #region State machine
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

    #region Minor Methods
    void EventSubscribing()
    {
        SceneEvents.GameStart.Event += GameStarts;
    }

    void EventUnsubscribing ()
    {
        SceneEvents.GameStart.Event -= GameStarts;
    }
    public void GameStarts()        => SwitchToNewState(PlayerStates.Jumping);
    public void Dashing()           => SwitchToNewState(PlayerStates.Dashing);
    public void PicksUpRocket()     => SwitchToNewState(PlayerStates.Rocketing);
    public void PicksUpBalloon()    => SwitchToNewState(PlayerStates.Balooning);
    public void PlayerDead()        => SwitchToNewState(PlayerStates.Inactive);

    void ExecuteMovement() => rb.velocity = velocity;
    bool HitsInteractableObject(Collider2D collider) => settings.InteractableLayer == (settings.InteractableLayer | 1 << collider.gameObject.layer);
    
    #endregion
}