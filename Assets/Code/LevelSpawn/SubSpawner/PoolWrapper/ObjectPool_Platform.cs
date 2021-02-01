using UnityEngine;
using System.Collections;

public class ObjectPool_Platform : ObjectPool_LevelObject
{
    //The level at which the platform will use the value endProbability, or the "max" probability
    const int LevelToReachEndProbability = 600;

    [Header("Spawn chance")]
    [SerializeField] float startingProbability;
    [SerializeField] float endProbability; 

    float ChangePerLevel => (endProbability - startingProbability) / LevelToReachEndProbability;

    public float Probability { get; private set; } //Current probability

    public void Initialize ()
    {
        //changePerLevel = (endProbability - startingProbability) / LevelToReachEndProbability;
    }

    public void CalculateSpawnProbability(int level)
    {
        if (level < LevelToReachEndProbability)
        {
            Probability = startingProbability + level * ChangePerLevel;
        }
    }
}