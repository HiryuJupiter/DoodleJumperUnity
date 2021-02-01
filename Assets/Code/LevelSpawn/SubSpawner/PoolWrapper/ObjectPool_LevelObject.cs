using UnityEngine;
using System.Collections;

public class ObjectPool_LevelObject : ObjectPool
{
    [SerializeField] InteractableTypes type;
    [SerializeField] float objectYSize;  //The size of the 

    public float ObjectYSize => objectYSize;
    public InteractableTypes Type => type;
}