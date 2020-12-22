using UnityEngine;
using System.Collections;

[RequireComponent(typeof(ScoreboardHighscoreText))]
public class ScoreBoard : MonoBehaviour
{
    [SerializeField] GameObject anyKeyToQuit;

    ScoreboardHighscoreText highscore;
    CanvasGroup cvs;

    bool waitingToQuit = false;

    #region MonoBehavior
    private void Awake()
    {
        //Reference
        highscore = GetComponent<ScoreboardHighscoreText>();
        cvs = GetComponent<CanvasGroup>();

        //Initialize
        CanvasGroupHelper.InstantHide(cvs);
        anyKeyToQuit.SetActive(false);
    }
    #endregion

    #region Public
    public void SetVisibility(bool reveal)
    {
    }

    public void ToReplayGame ()
    {
        SceneEvents.GameStart.InvokeEvent();
    }

    public void ToMainMenu ()
    {
        SceneEvents.GameOverBackToMain.InvokeEvent();
    }
    #endregion

    #region Canvas visibility
    void RevealCanvas()
    {
        StartCoroutine(CanvasGroupHelper.FadeInCoroutine(cvs, 0.1f));
        StartCoroutine(WaitForAnykeyToQuit());
    }

    void HideCanvas()
    {
        StartCoroutine(CanvasGroupHelper.FadeOutCoroutine(cvs, 0.1f));
        anyKeyToQuit.SetActive(false);
    }
    #endregion

    #region WaitForKey
    IEnumerator WaitForAnykeyToQuit ()
    {
        yield return new WaitForSeconds(2f);
        anyKeyToQuit.SetActive(true);
        waitingToQuit = true;
        while (waitingToQuit)
        {
            if (Input.anyKey)
            {
                ToMainMenu();
                waitingToQuit = false;
            }
            yield return null;
        }
    }
    #endregion
}