using UnityEngine;
using System.Collections;

[DefaultExecutionOrder(-10000)]
public class GameSettings : MonoBehaviour
{
    public static GameSettings Instance { get; set; }

    [Header("Layers")]
    [SerializeField] LayerMask interactableLayer;
    //[SerializeField] LayerMask platformLayer;
    public LayerMask InteractableLayer => interactableLayer;
    //public LayerMask PlatformLayer => platformLayer;

    [Header("Player Movement")]
    [SerializeField] float steerSpeed = 50f; 
    [SerializeField] float moveSpeed = 8f; 
    public float MoveAccceleration => steerSpeed;
    public float MoveSpeed => moveSpeed;

    [Header("Jump force")]
    [SerializeField] float jumpForceNormal = 11.7f;
    [SerializeField] float jumpForceStrong = 15f;
    [SerializeField] float jumpForceRainbow = 18f;
    public float JumpForceNormal => jumpForceNormal;
    public float JumpForceStrong => jumpForceStrong;
    public float JumpForceRainbow => jumpForceRainbow;

    [SerializeField] Vector2 bumperForce = new Vector2(2f, 15f);
    public Vector2 BumperForce => bumperForce;


    [Header("Jump duration")]
    [SerializeField] float jumpDuration_StarZoom = 0.29f;
    [SerializeField] float jumpDuration_Rainbow = 0.58f;
    public float JumpDuration_StarZoom => jumpDuration_StarZoom;
    public float JumpDuration_Rainbow => jumpDuration_Rainbow;

    [Header("Gravity")]
    [SerializeField] float maxFallSpeed = -20f;
    [SerializeField] float gravity = 25f;
    public float MaxFallSpeed => maxFallSpeed;
    public float Gravity => gravity;

    //
    public static GUIStyle GuiStyle;

    void Awake()
    {
        //Singleton
        if (Instance == null)
        {
            Instance = this;
        }

        //Initialize
        GuiStyle = new GUIStyle();
        GuiStyle.normal.textColor = Color.white;
        GuiStyle.fontSize = 40;
    }
}
