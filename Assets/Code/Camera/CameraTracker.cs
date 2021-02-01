using UnityEngine;
using System.Collections;

// 1. When the player is dead, let the camera


enum CameraMode { Standby, Climbing, Falling}

[DefaultExecutionOrder(-1)]
public class CameraTracker : MonoBehaviour
{
    const float FallingLerpSpeed = 10f;
    const float TopOfScreenOffset = 10f;

    public static float ScreenWidth;
    public static float TopOfScreen;
    public static float LeftOfScreen;
    public static float RightOfScreen;
    public static float BottomOfScreen;

    //Reference
    Transform character;

    //Status
    CameraMode cameraMode = CameraMode.Standby;

    //Cache
    float screenHalfWidth;
    float screenHalfHeight;
    
    float startingY;
    float startingTopOfScreen;

    void Start()
    {
        //Reference
        character = PlayerController.Instance.transform;

        //Initialize
        startingY = transform.position.y;

        startingTopOfScreen = TopOfScreen;
        ScreenWidth         = OrthographicCameraUtil.GetScreenWidth;
        screenHalfHeight    = OrthographicCameraUtil.GetScreenHeight / 2f;
        screenHalfWidth     = ScreenWidth / 2f;
        LeftOfScreen        = -screenHalfWidth;
        RightOfScreen       = screenHalfWidth;

        GameManager.OnGameStart += EnterClimbingMode;
        GameManager.OnGameOver  += EnterFallingMode;
    }

    void OnDisable()
    {
        GameManager.OnGameStart -= EnterClimbingMode;
        GameManager.OnGameOver  -= EnterFallingMode;
    }

    void Update()
    {
        UpdateTopOfScreen();
        UpdateBottomOfScreen();

        switch (cameraMode)
        {
            case CameraMode.Climbing:       ClimbingUpdate();       break;
            case CameraMode.Falling:        FallingUpdate();        break;
        }
    }

    #region Private
    void ClimbingUpdate ()
    {
        //Check if the player has reached a new highest point
        if (character.position.y > transform.position.y)
        {
            SetCameraPositionY(character.position.y);
        }
    }

    void FallingUpdate()
    {
        float targetY = Mathf.Lerp(transform.position.y, startingY, FallingLerpSpeed * Time.deltaTime);
        if (targetY <= startingY)
        {
            EnterStandbyMode();
        }

        /*
         float targetY = Mathf.Lerp(transform.position.y, character.position.y, FallingLerpSpeed * Time.deltaTime);
        if (targetY < startingY)
        {
            targetY = startingY;
            EnterStandbyMode();
        }
         */
        SetCameraPositionY(targetY);
        
    }
    #endregion

    #region Minor methods
    void SetCameraPositionY(float y)
    {
        transform.position = new Vector3(0f, y, transform.position.z);
    }

    void EnterClimbingMode()        => cameraMode = CameraMode.Climbing;
    void EnterFallingMode ()        => cameraMode = CameraMode.Falling;
    void EnterStandbyMode()
    {
        SetCameraPositionY(startingY);
        cameraMode = CameraMode.Standby;
    }

    void UpdateTopOfScreen ()       => TopOfScreen      = transform.position.y + screenHalfHeight + TopOfScreenOffset;
    void UpdateBottomOfScreen ()    => BottomOfScreen   = transform.position.y - screenHalfHeight;
    #endregion
}