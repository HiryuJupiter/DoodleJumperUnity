using UnityEngine;
using System.Collections;

[RequireComponent(typeof(ScoreboardHighscoreText))]
public class Scoreboard : MonoBehaviour
{
    [SerializeField] GameObject anyKeyToQuit;
    [SerializeField] CanvasGroup canvasGroup;

    ScoreboardHighscoreText highscore;
    GameManager gm;

    bool waitingToQuit = false;

    #region MonoBehavior
    private void Awake()
    {
        //Reference
        highscore = GetComponent<ScoreboardHighscoreText>();

        //Initialize
        CanvasGroupHelper.InstantHide(canvasGroup);
        anyKeyToQuit.SetActive(false);
    }

    void Start()
    {
        gm = GameManager.Instance;
    }
    #endregion

    #region Public
    public void Open ()
    {
        StartCoroutine(OpenScoreboard());
        OpenScoreboard();
    }

    public void Close ()
    {
        CloseScoreboard();
    }
    #endregion

    #region Canvas visibility
    IEnumerator OpenScoreboard()
    {
        StartCoroutine(CanvasGroupHelper.FadeInCoroutine(canvasGroup, 0.1f));
        yield return new WaitForSeconds(0.2f);

        highscore.DisplayHighscore((int)gm.currentScore);

        //StartCoroutine(WaitForAnykeyToQuit());
    }

    //IEnumerator WaitForAnykeyToQuit ()
    //{
    //    yield return new WaitForSeconds(2f);

    //    anyKeyToQuit.SetActive(true);
    //    waitingToQuit = true;

    //    while (waitingToQuit)
    //    {
    //        if (Input.anyKey)
    //        {
    //            gm.ScoreBoardBackToMain();
    //            waitingToQuit = false;
    //        }
    //        yield return null;
    //    }
    //}

    void CloseScoreboard()
    {
        StartCoroutine(CanvasGroupHelper.FadeOutCoroutine(canvasGroup, 0.1f));
        anyKeyToQuit.SetActive(false);
    }
    #endregion
}