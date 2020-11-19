using UnityEngine;
using System.Collections;

//This makes this class able to be stored in serialization system, such as
//... Unity inspector, Binary, or JSON
[System.Serializable]
public class Character : MonoBehaviour
{
    public string name;
    public int health;
    public int mana;
    public int level;
    public float experience;

    public Character()
    {
        this.name = "";
    }

    void Start()
    {

    }

    void Update()
    {

    }
}