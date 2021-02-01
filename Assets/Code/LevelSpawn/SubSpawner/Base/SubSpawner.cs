using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(LevelSpawnManager))]
public abstract class SubSpawner : MonoBehaviour
{
    protected LevelSpawnManager spawnManager;

    protected virtual void Awake()
    {
        spawnManager = GetComponent<LevelSpawnManager>();
    }

    protected void OnSpawnedPlatform (float platformSize)
    {
        spawnManager.IncrementSpawnnedFloors();
        spawnManager.IncrementNextSpawnPosY(platformSize);
    }

    protected ObjectPool_Platform RandomlySelectPlatformAmongWeightedOptions (List<ObjectPool_Platform> platforms, int spawnedFloors)
    {
        float totalProbabilityRange = 0; //Sum of all probability weights
        foreach (var p in platforms)
        {
            p.CalculateSpawnProbability(spawnedFloors);
            totalProbabilityRange += p.Probability;
        }

        float random = Random.Range(0f, totalProbabilityRange);
        float upto = 0; //Calculated positions

        foreach (var p in platforms)
        {
            if (p.Probability + upto > random)
                return p;
            upto += p.Probability;
        }

        Debug.LogError("This shouldn't happen");
        return platforms[platforms.Count - 1];
    }
}