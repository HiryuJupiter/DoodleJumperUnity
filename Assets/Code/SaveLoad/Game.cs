using UnityEngine;
using System.Collections;

//This makes this class able to be stored in serialization system, such as
//... Unity inspector, Binary, or JSON
[System.Serializable]
public class Game : MonoBehaviour
{
    public static Game current;
    public Character knight;


    public Game()
    {
        knight = new Character();
    }

    void Start()
    {

    }

    void Update()
    {

    }
}