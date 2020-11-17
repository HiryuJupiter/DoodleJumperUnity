using UnityEngine;
using UnityEngine.UI;

namespace AIAssignment3
{
    [RequireComponent(typeof(HUDSpawnButtons))]
    public class UIManager : MonoBehaviour
    {
        //Lazy singleton
        public static UIManager instance;

        //Fields
        [SerializeField] Text debugText;

        HUDSpawnButtons spawnButtons;

        void Awake()
        {
            //Initialize
            instance = this;
            spawnButtons = GetComponent<HUDSpawnButtons>();
            ExitSpawningMode();
        }

        public void EnterSpawningMode(SpawningMode mode)
        {
            //Entering a spawning mode for spawning lifeformes
            Debug.Log("EnterSpawningMode :" + mode);
            spawnButtons.RevealButtonBorder(mode);
            DisplayDebugText("Click and hold left-mouse-button to spawn lifeform!");
        }

        public void ExitSpawningMode()
        {
            //To exit spawning mode, we tell the spawn button manager to hide all borders 
            spawnButtons.HideButtonBorder();
            DisplayDebugText("");
        }

        void DisplayDebugText(string text)
        {
            //Erase debug text's text box
            debugText.text = text;
        }


    }

}