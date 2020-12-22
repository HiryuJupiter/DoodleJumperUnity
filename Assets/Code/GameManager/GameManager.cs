using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    //Consts
    const int SceneIndex_MainMenu = 0;

    //Reference
    UIManager ui;
    CameraTracker cam;
    Transform character;

    //Status

    //Properties
    public static GameStates gameState { get; private set; } = GameStates.MainMenu;
    public float playerHighestY { get; private set; }
    public int TimeElapsed { get; private set; }
    public int Coins { get; private set; }


    #region MonoBehavior
    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        //Reference
        ui = UIManager.instance;
        cam = CameraTracker.Instance;
        character = PlayerController.Instance.transform;

        EventSubscribing();
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
        if (playerHighestY > CurrentHighscore)
        {
            HighScore_PlayerPref.SaveHighscore((int)playerHighestY);
        }
    }

    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene(SceneIndex_MainMenu);
    }

    public void ReplayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    #endregion

    void Reset()
    {
        gameState = GameStates.MainMenu;
        playerHighestY = 0;
    }

    #region Game start
    void GameStart()
    {
        gameState = GameStates.Gameplay;
        StartCoroutine(DelayedStartRunning());
    }

    IEnumerator DelayedStartRunning()
    {
        yield return new WaitForSeconds(1f);
        SceneEvents.GameStart.InvokeEvent();
    }
    #endregion

    #region Private 
    int CurrentHighscore => HighScore_PlayerPref.LoadHighScore();

    void UpdateHighestCharacterPosition()
    {
        if (character.position.y > playerHighestY)
        {
            playerHighestY = character.position.y;
            ui.SetCurrentScore((int)playerHighestY);
            cam.SetCameraPositionY(playerHighestY);
        }
    }
    #endregion

    #region Minor Methods
    void EventSubscribing()
    {
        SceneEvents.PlayerDead.Event += GameLost;
    }

    void OnDisable()
    {
        //Event unsubscribing
        SceneEvents.PlayerDead.Event -= GameLost;
    }

    void GameOverBackToMain() => gameState = GameStates.MainMenu;
    #endregion
}