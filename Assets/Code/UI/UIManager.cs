using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    ScoreBoard scoreboard;
    HUDManager hud;
    MainMenuManager mainmenu;

    #region MonoBehavior
    void Awake()
    {
        //Initialize
        instance = this;

        //Ref
        hud         = GetComponentInChildren<HUDManager>();
        scoreboard  = GetComponentInChildren<ScoreBoard>();
        mainmenu    = GetComponentInChildren<MainMenuManager>();
    }

    void Start()
    {
        //Event subscribing
        SceneEvents.PlayerDead.Event            += PlayerDead;
        SceneEvents.GameStart.Event             += MainMenuClicksGameStart;
        SceneEvents.GameOverBackToMain.Event    += GameOverBackToMain;
    }

    void OnDisable()
    {
        //Event unsubscribing
        SceneEvents.PlayerDead.Event            -= PlayerDead;
        SceneEvents.GameStart.Event             -= MainMenuClicksGameStart;
        SceneEvents.GameOverBackToMain.Event    -= GameOverBackToMain;
    }
    #endregion

    //Public
    public void SetCurrentScore(int score)
    {
        hud.SetCurrentScore(score);
    }

    //Private
    void PlayerDead ()
    {
        scoreboard.SetVisibility(true);
    }

    void MainMenuClicksGameStart ()
    {
        mainmenu.SetVisibility(false);
    }

    void GameOverBackToMain ()
    {
        scoreboard.SetVisibility(false);
        mainmenu.SetVisibility(true);
    }

    void RevealMainMenu()   => mainmenu.SetVisibility(true);
    void HideMainMenu()     => mainmenu.SetVisibility(false);
    void RevealScoreboard() => scoreboard.SetVisibility(true);
    void HideScoreboard()   => scoreboard.SetVisibility(false);
    void RevealHUD()        => hud.SetVisibility(true);
    void HideHUD()          => hud.SetVisibility(false);
}