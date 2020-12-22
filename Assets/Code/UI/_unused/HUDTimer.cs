using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HUDTimer : MonoBehaviour
{
    [SerializeField] Text timer;
    float timeElapsed;


    public void UpdateTimer()
    {
        timeElapsed += Time.deltaTime;
        string minutes = Mathf.Floor(timeElapsed / 60f).ToString("00");
        string seconds = Mathf.Floor(timeElapsed % 60).ToString("00");
        string miliseconds = Mathf.Floor((timeElapsed * 100) % 100).ToString("00");

        timer.text = minutes + ":" + seconds + ":" + miliseconds;
    }
    public void ResetTimer()
    {
        timeElapsed = 0f;
    }
}