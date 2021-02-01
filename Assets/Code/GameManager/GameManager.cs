using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    //Event
    public delegate void GameStartHandler();
    public delegate void PlayerDeadHandler();
    public delegate void ScoreBoardBackToMainHandler();

    public static event GameStartHandler               OnGameStart;
    public static event PlayerDeadHandler              OnGameOver;
    public static event ScoreBoardBackToMainHandler    OnScoreboardBackToMain;

    //Consts
    const int SceneIndex_MainMenu = 0;

    //Variables
    public static GameManager Instance;

    UIManager ui;
    Transform character;

    //Properties
    public static GameStates gameState { get; private set; } = GameStates.MainMenu;
    public float currentScore { get; private set; }
    

    #region MonoBehavior
    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        //Reference
        ui = UIManager.instance;
        character = PlayerController.Instance.transform;
    }

    void Update()
    {
        switch (gameState)
        {
            case GameStates.MainMenu:
                break;
            case GameStates.Gameplay:   UpdateHighestCharacterPosition();
                break;
            case GameStates.Scoreboard:
                break;
        }
    }

    void OnGUI()
    {
        GUI.Label(new Rect(20, 300, 500, 50), "gameState: " + gameState, GameSettings.GuiStyle);
    }
    #endregion

    #region Public 
    public void GameStart()
    {
        gameState = GameStates.Gameplay;
        currentScore = 0;

        OnGameStart?.Invoke();
    }

    public void PlayerDead()
    {
        StartCoroutine(ShowScoreBoard());
    }

    
    #endregion

    #region Private 
    int CurrentHighscore => HighScore_PlayerPref.LoadHighScore();

    void UpdateHighestCharacterPosition()
    {
        if (character.position.y > currentScore)
        {
            currentScore = character.position.y;
            ui.SetCurrentScore((int)currentScore);
        }
    }
    #endregion

    #region Minor Methods
    IEnumerator ShowScoreBoard ()
    {
        gameState = GameStates.Scoreboard;
        SaveHighScoreToPlayerPref();

        OnGameOver?.Invoke();

        yield return new WaitForSeconds(1.5f);
        ScoreBoardBackToMain();
    }

    void ScoreBoardBackToMain()
    {
        gameState = GameStates.MainMenu;
        currentScore = 0;
        OnScoreboardBackToMain?.Invoke();
    }

    void SaveHighScoreToPlayerPref ()
    {
        if (currentScore > CurrentHighscore)
        {
            HighScore_PlayerPref.SaveHighscore((int)currentScore);
        }
    }
    #endregion
}