using UnityEngine;
using System.Collections;

//This class is referenced by other class's Awake
[DefaultExecutionOrder(-1000)]
public class InputManager : MonoBehaviour
{
    public static InputManager Instance;

    public float MoveX { get ; private set; }
    public bool Jump { get; private set;}
    public bool PressedMoveKey => MoveX != 0;

    void Awake()
    {
        Instance = this;
    }

    void Update()
    {
        UpdateHorizontal();
        UpdateJump ();
    }

    void UpdateHorizontal ()
    {
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            MoveX = -1;
        }
        else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            MoveX = 1;
        }
        else
        {
            MoveX = 0;
        }
    }

    void UpdateJump()
    {
        Jump = Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.W);
    }
}