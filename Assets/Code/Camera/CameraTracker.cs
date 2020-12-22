using UnityEngine;
using System.Collections;

[DefaultExecutionOrder(-1)]
public class CameraTracker : MonoBehaviour
{
    public static CameraTracker Instance;

    //Reference
    Transform character;
    Transform camera;

    //Status
    float highestCharacterY;

    //Cache
    float screenHalfHeight;

    //Properties
    public float BottomOfScreen => highestCharacterY - screenHalfHeight;
    public float TopOfScreen => highestCharacterY + screenHalfHeight;
    public float HighestCharacterPosY => highestCharacterY;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        //Reference
        character = PlayerController.Instance.transform;
        camera = Camera.main.transform;

        //Initialize
        highestCharacterY = transform.position.y;
        screenHalfHeight = OrthographicCameraUtil.GetScreenHeight / 2f;
    }

    void Update()
    {
        UpdateHighestCharacterPosition();
    }

    void UpdateHighestCharacterPosition()
    {
        //Constantly check if the player has reached a new highest point
        if (character.position.y > highestCharacterY)
        {
            highestCharacterY = character.position.y;
            SetCameraPositionY(highestCharacterY);
        }
    }

    public void SetCameraPositionY(float y)
    {
        //Move the camera along with the player's upward movement
        camera.position = new Vector3(0f, y, camera.position.z);
    }
}