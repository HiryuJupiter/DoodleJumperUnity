using UnityEngine;
using System.Collections;

//Make this script execute earlier than other scripts
[DefaultExecutionOrder(-1000)]
public class InputManager : MonoBehaviour
{
    public static InputManager Instance;

    //Property - allows reading the value publically, but can only set it privately within this class
    public float MoveX { get ; private set; }
    public bool Jump { get; private set;}

    void Awake()
    {
        //Lazy singleton
        Instance = this;
    }

    void Update()
    {
        UpdateMovement();
        UpdateJump ();
    }

    void UpdateMovement ()
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