using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEditor;

public class MainMenuManager : MonoBehaviour
{
    //Fields
    [SerializeField] CanvasGroup RootMenu;
    [SerializeField] CanvasGroup InfoPage;
    [SerializeField] Text text_highscore;

    GameManager gm;

    #region MonoBehavior
    void Start()
    {
        //Ref
        gm = GameManager.Instance;

        //Initialize
        OpenRootMenu();
    }
    #endregion

    #region Public 
    public void StartGame ()
    {
        gm.GameStart();
    }

    public void OpenRootMenu()
    {
        RevealMainMenu();
        UpdateScoreDisplay();
    }

    public void CloseRootMenu()
    {
        HideRootMenu();
    }

    public void OpenInfoPage ()
    {
        RevealInfoPage();
        HideRootMenu();
    }

    public void CloseInfoPage ()
    {
        HideInfoPage();
        RevealMainMenu();
    }
    #endregion

    #region Minor methods 
    void UpdateScoreDisplay()
    {
        int score = HighScore_PlayerPref.LoadHighScore();
        string paddedString = score.ToString().PadLeft(6, '0');
        text_highscore.text = paddedString;
    }

    void RevealMainMenu () => CanvasGroupUtil.RevealCanvasGroup(RootMenu);
    void HideRootMenu () => CanvasGroupUtil.HideCanvasGroup(RootMenu);
    void RevealInfoPage() => CanvasGroupUtil.RevealCanvasGroup(InfoPage);
    void HideInfoPage() => CanvasGroupUtil.HideCanvasGroup(InfoPage);
    #endregion
}