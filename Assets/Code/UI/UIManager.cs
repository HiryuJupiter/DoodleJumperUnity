using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    [SerializeField] Text highScore;
    [SerializeField] GameObject gameOverScreen;

    void Awake()
    {
        //Lazy singleton
        instance = this;

        //Hide unnecessary elements
        gameOverScreen.SetActive(false);
    }

    public void GameOver(int currentScore)
    {
        //Hide unnecessary elements and show highscore
        gameOverScreen.SetActive(true);

        //Update score display
        highScore.text = currentScore.ToString();
    }

    public void SetCurrentScore (int score)
    {
        //Display the highscore with some paddings to the left
        highScore.text = score.ToString();
    }
}