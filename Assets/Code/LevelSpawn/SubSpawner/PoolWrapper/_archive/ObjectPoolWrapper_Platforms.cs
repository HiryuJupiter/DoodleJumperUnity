using UnityEngine;
using System.Collections;

[CreateAssetMenu(fileName = "Platform", menuName = "Platforms/PlatformPrefabWrapper")]
public class ObjectPoolWrapper_Platforms : ScriptableObject
{
    //The level at which the platform will use the value endProbability, or the "max" probability
    const int LevelToReachEndProbability = 600;

    [SerializeField] PoolableObject prefab;
    [SerializeField] InteractableTypes type;
    [SerializeField] float objectYSize;  //The size of the 

    [Header("Spawn chance")]
    [SerializeField] float startingProbability;
    [SerializeField] float endProbability;

    ObjectPool pool;

    //Cache
    float changePerLevel;

    public float Probability { get; private set; } //Current probability
    public float ObjectYSize => objectYSize;
    public InteractableTypes Type => type;

    public void Initialize()
    {
        changePerLevel = (endProbability - startingProbability) / LevelToReachEndProbability;
    }

    public void CalculateSpawnProbability(int level)
    {
        if (level < LevelToReachEndProbability)
        {
            Probability = startingProbability + level * changePerLevel;
        }
    }

    public void Spawn (Vector2 pos)
    {
        pool.Spawn(pos);
    }

    public void DespawnAll ()
    {
        pool.DespawnAll();
    }
}