using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SocialPlatforms.Impl;

public class HUDManager : MonoBehaviour
{
    public static HUDManager instance;

    [SerializeField] GameObject HUD_Group;
    [SerializeField] Text coins;
    [SerializeField] Text score;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        HideHUD();
    }

    #region Public
    public void SetVisibility(bool reveal)
    {
    }


    public void SetCoins (int amount)
    {
        coins.text = amount.ToString();
    }
    public void SetCurrentScore(int score)
    {
        this.score.text = score.ToString();
    }
    #endregion


    void HideHUD() => HUD_Group.SetActive(false);
    void RevealHUD() => HUD_Group.SetActive(true);
}