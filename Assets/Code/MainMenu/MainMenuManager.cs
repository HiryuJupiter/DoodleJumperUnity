using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEditor;

public class MainMenuManager : MonoBehaviour
{
    //Const
    const string Key_VolumnSliderValue = "Master";
    
    const int SceneIndex_GamePlay = 1;

    //Fields
    [SerializeField] AudioMixer mixer;
    [SerializeField] Slider volumnSlider;
    [SerializeField] Text highScore;
    [SerializeField] CanvasGroup MainMenu;
    [SerializeField] CanvasGroup OptionsMenu;
    [SerializeField] CanvasGroup AboutMenu;

    //Cache
    int highscore;

    #region MonoBehavior
    private void Awake()
    {
        //Reveal main menu
        CanvasGroupUtil.HideCanvasGroup(OptionsMenu);
        CanvasGroupUtil.HideCanvasGroup(AboutMenu);
        CanvasGroupUtil.RevealCanvasGroup(MainMenu);

        //Update highscore display
        highscore = HighScore_PlayerPref.LoadHighScore();
        string paddedString = highscore.ToString().PadLeft(6, '0'); 
        highScore.text = paddedString;
    }

    void Start()
    {
        //Initialize volumn 
        LoadVolumnFromPlayerPrefs();
    }
    #endregion

    #region Public - UI button interactions
    public void SetVisibility(bool reveal)
    {
    }

    public void StartGame ()
    {
        //Load the game
        SceneManager.LoadScene(SceneIndex_GamePlay);
    }

    public void Open_OptionsMenu ()
    {
        CanvasGroupUtil.HideCanvasGroup(MainMenu);
        CanvasGroupUtil.RevealCanvasGroup(OptionsMenu);
    }

    public void Open_AboutMenu()
    {
        CanvasGroupUtil.HideCanvasGroup(MainMenu);
        CanvasGroupUtil.RevealCanvasGroup(AboutMenu);
    }

    public void OptionsMenu_BackToMain ()
    {
        CanvasGroupUtil.HideCanvasGroup(OptionsMenu);
        CanvasGroupUtil.RevealCanvasGroup(MainMenu);
    }

    public void AboutMenu_BackToMain()
    {
        CanvasGroupUtil.HideCanvasGroup(AboutMenu);
        CanvasGroupUtil.RevealCanvasGroup(MainMenu);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void VolumnSliderUpdate(float value)
    {
        SetMixerVolumn(value);
        SaveVolumnToPlayerPrefs(value);
    }
    #endregion

    void LoadVolumnFromPlayerPrefs ()
    {
        float value = PlayerPrefs.GetFloat(Key_VolumnSliderValue, 1);
        volumnSlider.SetValueWithoutNotify(value);
        SetMixerVolumn(value);
    }

    void SaveVolumnToPlayerPrefs (float value)
    {
        PlayerPrefs.SetFloat(Key_VolumnSliderValue, value);
    }

    void SetMixerVolumn (float value)
    {
        mixer.SetFloat(Key_VolumnSliderValue, Mathf.Log10(value) * 20); 
    }
}