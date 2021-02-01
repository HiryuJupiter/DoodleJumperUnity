using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class MainMenu_VolumnToggle : MonoBehaviour
{
    private const string Key_Volumn = "Master";

    [SerializeField] private Sprite speakerIcon_On;
    [SerializeField] private Sprite speakerIcon_Off;
    [SerializeField] private Image  speakerImage;
    [SerializeField] private AudioMixer mixer;

    private bool isOn = true;

    public void Toggle ()
    {
        speakerImage.sprite = (isOn = !isOn) ? speakerIcon_On : speakerIcon_Off;
        mixer.SetFloat(Key_Volumn, isOn ? 0 : -80);
    }
}