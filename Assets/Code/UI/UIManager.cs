using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    GameManager gm;

    MainMenuManager mainmenu;
    HUDManager hud;
    Scoreboard scoreboard;

    #region MonoBehavior
    void Awake()
    {
        //Initialize
        instance = this;

        //Ref
        gm = GameManager.Instance;

        mainmenu    = GetComponentInChildren<MainMenuManager>();
        hud         = GetComponentInChildren<HUDManager>();
        scoreboard  = GetComponentInChildren<Scoreboard>();
    }

    void Start()
    {
        GameManager.OnGameStart             += GameStart;
        GameManager.OnGameOver              += GameOver;
        GameManager.OnScoreboardBackToMain  += ScoreboardBackToMain;
    }

    void OnDisable()
    {
        GameManager.OnGameStart             -= GameStart;
        GameManager.OnGameOver              -= GameOver;
        GameManager.OnScoreboardBackToMain  -= ScoreboardBackToMain;
    }
    #endregion

    #region Public
    public void SetCurrentScore(int score)
    {
        hud.SetDistanceScore(score);
    }
    #endregion

    #region Private
    void GameStart()
    {
        HideMainMenu();
        RevealHUD();
    }

    void GameOver()
    {
        RevealScoreboard();
        HideHUD();
    }

    void ScoreboardBackToMain()
    {
        RevealMainMenu();
        HideScoreboard();
    }
    #endregion

    #region Minor
    void RevealMainMenu() => mainmenu.OpenRootMenu();
    void HideMainMenu() => mainmenu.CloseRootMenu();
    void RevealScoreboard() => scoreboard.Open();
    void HideScoreboard() => scoreboard.Close();
    void RevealHUD() => hud.SetVisibility(true);
    void HideHUD() => hud.SetVisibility(false);
    #endregion
}