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
        //Reference
        ui = UIManager.instance;
        cam = CameraTracker.Instance;
        character = CharacterController.Instance.transform;
    }

    void Update()
    {
        //Constantly check if character has reached a higher position
        UpdateHighestCharacterPosition();
    }
    #endregion

    #region Public 
    public void GameLost()
    {
        //When being told that the game has lost, tell UI Manager to show game over screen
        ui.GameOver((int)highestCharacterY);

        //Update highscore if we've broken previous highscore
        if (highestCharacterY > CurrentHighscore)
        {
            HighScore.SaveHighscore((int)highestCharacterY);
        }
    }

    public void ReturnToMainMenu()
    {
        //Load main menu scene
        SceneManager.LoadScene(SceneIndex_MainMenu);
    }

    public void ReplayGame()
    {
        //Load current scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    #endregion

    #region Private 
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
    #endregion
}