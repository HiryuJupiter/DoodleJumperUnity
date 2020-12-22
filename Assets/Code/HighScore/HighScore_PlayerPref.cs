using UnityEngine;
using System.Collections;

public class HighScore_PlayerPref : MonoBehaviour
{
    const string Key_HighScore = "HighScore";
    public static void SaveHighscore(int score)
    {
        PlayerPrefs.SetInt(Key_HighScore, score);
    }

    public static int LoadHighScore()
    {
        return PlayerPrefs.GetInt(Key_HighScore, 0);
    }
}