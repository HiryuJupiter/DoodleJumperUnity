using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using System.Collections;

public class MainMenu_VolumnControl : MonoBehaviour
{
    const string Key_Volumn = "Master";

    [SerializeField] AudioMixer mixer;
    [SerializeField] Slider volumnSlider;

    float volumn;


    //Mono
    void Start()
    {
        SetVolumnFromPlayerPrefs();

    }

    //Public 
    public void VolumnSliderUpdate(float value)
    {
        volumn = value;
        SetMixerVolumn();
    }
    public void Save()
    {
        PlayerPrefs.SetFloat(Key_Volumn, volumn);
    }

    //Private
    void SetVolumnFromPlayerPrefs()
    {
        volumn = PlayerPrefs.GetFloat(Key_Volumn, 1);
        volumnSlider.SetValueWithoutNotify(volumn);
        SetMixerVolumn();
    }

    void SetMixerVolumn()
    {
        mixer.SetFloat(Key_Volumn, Mathf.Log10(volumn) * 20);
    }
}