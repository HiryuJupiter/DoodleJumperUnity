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
        if (!isGameLost)
        {
            HorizontalInputUpdate();
            
            
        }
    }

    void FixedUpdate()
    {
        ApplyGravity();

        if (!isGameLost)
        {
            CheckIfFallenToDeath();
            ExecuteMovement();
        }
    }

    void OnTriggerStay2D(Collider2D collider)
    {
        if (Falling && HitsPlatformCollider(collider))
        {
            Jump();
            animator.Lands();
            collider.GetComponent<Platform>().Destroy();
        }
    }
    #endregion

    void CheckIfFallenToDeath()
    {
        if (transform.position.y < cam.BottomOfScreen)
        {
            isGameLost = true;
            sceneManager.GameLost();
        }
    }

    void HorizontalInputUpdate()
    {
        velocity.x = Mathf.Lerp(velocity.x, input.MoveX * xMoveSpeed, xAcceleration * Time.deltaTime);

        //Set facing
        if (input.MoveX != 0)
        {
            animator.SetFacing(input.MoveX > 0);
        }
    }

    void Jump() => velocity.y = jumpForce;
    void ApplyGravity() => velocity.y -= gravity * Time.deltaTime;
    void ExecuteMovement() => rb.velocity = velocity;
    bool HitsPlatformCollider(Collider2D collider) => platformLayer == (platformLayer | 1 << collider.gameObject.layer);
}