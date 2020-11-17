using UnityEngine;
using System.Collections;

namespace AIAssignment3
{
    //A class for storing scene settings for all classes to easily reference
    public class Settings : MonoBehaviour
    {
        //Lazy singleton
        public static Settings instance;

        //Layer masks variables
        [SerializeField] LayerMask layer_hideSpot;

        [SerializeField] LayerMask layer_Obstacle;

        //Public properties for referencing private variables
        public LayerMask Layer_HideSpot => layer_hideSpot;
        public LayerMask Layer_Obstacle => layer_Obstacle;

        void Awake()
        {
            //Lazy singleton
            instance = this;
        }
    }
}