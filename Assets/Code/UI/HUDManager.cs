using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SocialPlatforms.Impl;

public class HUDManager : MonoBehaviour
{
    [SerializeField] CanvasGroup HUD;
    [SerializeField] Text distance;

    public void SetDistanceScore(int score)
    {
        distance.text = score.ToString()  + "m";
    }

    public void SetVisibility(bool isVisibile)
    {
        if (isVisibile)
        {
            CanvasGroupUtil.RevealCanvasGroup(HUD);
        }
        else
        {
            CanvasGroupUtil.HideCanvasGroup(HUD);
        }
    }
}