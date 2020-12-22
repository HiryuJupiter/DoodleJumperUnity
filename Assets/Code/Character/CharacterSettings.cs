using UnityEngine;
using System.Collections;

[DefaultExecutionOrder(-10000)]
public class CharacterSettings : MonoBehaviour
{
    public static CharacterSettings Instance { get; set; }

    [Header("Layers")]
    [SerializeField] LayerMask interactableLayer;
    public LayerMask InteractableLayer => interactableLayer;

    [Header("Player Movement")]
    [SerializeField] float steerSpeed = 2f; 
    [SerializeField] float moveSpeed = 10f; 
    public float MoveAccceleration => steerSpeed;
    public float MoveSpeed => moveSpeed;

    [Header("Normal Jump")]
    [SerializeField] float normalJumpForce = 12f;
    [SerializeField] float superJumpForce = 22f;
    public float NormalJumpForce => normalJumpForce;
    public float SuperJumpForce => superJumpForce;

    [Header("Gravity")]
    [SerializeField] float maxFallSpeed = -15f;
    [SerializeField] float gravity = 80f;
    public float MaxFallSpeed => maxFallSpeed;
    public float Gravity => gravity;

    void Awake()
    {
        //Singleton
        if (Instance == null)
        {
            Instance = this;
        }
    }
}
