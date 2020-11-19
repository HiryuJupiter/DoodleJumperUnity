using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameplaySceneManager : MonoBehaviour
{
    public static GameplaySceneManager Instance;

    //Consts
    const int SceneIndex_MainMenu = 0;

    //Reference
    UIManager ui;
    CameraTracker cam;
    Transform character;

    //Status
    float highestCharacterY;
    #region MonoBehavior
    void Awake()
    {
        //Lazy singleton
        Instance = this;
    }

    void Start()
    {
        ui = UIManager.instance;
        cam = CameraTracker.Instance;
        character = CharacterController.Instance.transform;
    }

    void Update()
    {
        UpdateHighestCharacterPosition();
    }
    #endregion

    public void GameLost ()
    {
        ui.GameOver((int)highestCharacterY);

        if (highestCharacterY > CurrentHighscore)
        {
            HighScore.SaveHighscore((int)highestCharacterY);
        }
    }

    public void ReturnToMainMenu ()
    {
        SceneManager.LoadScene(SceneIndex_MainMenu);
    }

    public void ReplayGame ()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    int CurrentHighscore => HighScore.LoadHighScore();

    void UpdateHighestCharacterPosition()
    {
        //Save high score if it is higher than previous highscore
        if (character.position.y > highestCharacterY)
        {
            highestCharacterY = character.position.y;
            ui.SetCurrentScore((int)highestCharacterY);
            cam.SetCameraPositionY(highestCharacterY);
        }
    }
}