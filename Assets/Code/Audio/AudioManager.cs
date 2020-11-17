using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

namespace AIAssignment3
{
    public class AudioManager : MonoBehaviour
    {
        //Variables
        public AudioMixer mixer;
        public Slider  volumnSlider;

        //Allows the player to use a slider to set the audio volumn
        public void SetMixerVolumn (float vol)
        {
            mixer.SetFloat("Volumn", vol);
        }

        //Toggle BGM on and off
        public void ToggleAudio (bool isOn)
        {
            SetMixerVolumn(isOn ? 1 : -80);
            SetSlider (isOn ? 1 : -80);
        }

        //A seperate method for setting slider position
        void SetSlider (float value)
        { 
            volumnSlider.value = value;
        }
    }
}