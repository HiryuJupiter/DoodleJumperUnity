using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class CharacterController : MonoBehaviour
{
    public static CharacterController Instance;

    [Header("Stats")]
    [SerializeField] float xAcceleration = 2f;
    [SerializeField] float xMoveSpeed = 10f;
    [SerializeField] float gravity = 2f;
    [SerializeField] float jumpForce = 5f;

    [Header("Layer masks")]
    [SerializeField] LayerMask platformLayer;

    //Reference
    InputManager input;
    Rigidbody2D rb;
    CameraTracker cam;
    GameplaySceneManager sceneManager;
    CharacterAnimationController animator;

    //Status
    Vector2 velocity = Vector2.zero;
    bool isGameLost;

    //Properties
    bool Falling => velocity.y < 0;

    #region Monobehavior
    void Awake()
    {
        //Lazy singleton
        Instance = this;
    }

    void Start()
    {
        //Reference
        input = InputManager.Instance;
        cam = CameraTracker.Instance;
        sceneManager = GameplaySceneManager.Instance;
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponentInChildren<CharacterAnimationController>();

        //Initialize
        velocity.y = jumpForce; //Propell the player onto screen when the game starts
        ExecuteMovement();
    }

    void Update()
    {
        //Only allow control when game is not lost
        if (!isGameLost)
        {
            HorizontalInputUpdate();
        }
    }

    void FixedUpdate()
    {
        if (!isGameLost)
        {
            ApplyGravity();
            CheckIfFallenToDeath();
            ExecuteMovement();
        }
    }

    void OnTriggerStay2D(Collider2D collider)
    {
        //If we hit a platform trigger while falling, do jump and destroy the platform.
        if (Falling && HitsPlatformCollider(collider))
        {
            Jump();
            animator.PlayLanding();
            collider.GetComponent<Platform>().Destroy();
        }
    }
    #endregion

    void CheckIfFallenToDeath()
    {
        //If character falls below screen, game's over.
        if (transform.position.y < cam.BottomOfScreen)
        {
            isGameLost = true;
            sceneManager.GameLost();
        }
    }

    void HorizontalInputUpdate()
    {
        //Use a lerp on character movement to add a bit of challenge.
        velocity.x = Mathf.Lerp(velocity.x, input.MoveX * xMoveSpeed, xAcceleration * Time.deltaTime);

        //Set facing based on input.
        if (input.MoveX != 0)
        {
            animator.SetFacing(input.MoveX > 0);
        }
    }

    //Expression body methods for self documenting code
    void Jump() => velocity.y = jumpForce;
    void ApplyGravity() => velocity.y -= gravity * Time.deltaTime;
    void ExecuteMovement() => rb.velocity = velocity;
    bool HitsPlatformCollider(Collider2D collider) => platformLayer == (platformLayer | 1 << collider.gameObject.layer);
}