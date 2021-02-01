using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class StartingZoneSubSpawner : SubSpawner
{
    const float spawnBoundX = 2.2f;

    [Header("Platforms")]
    [SerializeField] ObjectPool_Platform long_basicPlatform;
    [SerializeField] ObjectPool_Platform long_extraHighPlatform;
    [SerializeField] ObjectPool_Platform long_starZoomPlatform;

    List<ObjectPool_Platform> platforms;


    //Spawn positions
    float[] xPositions = new float[3];


    protected override void Awake()
    {
        base.Awake();

        platforms = new List<ObjectPool_Platform>
        {
            long_basicPlatform,
            long_extraHighPlatform,
            long_starZoomPlatform
        };

        // Initialize spawning positions
        xPositions[0] = -spawnBoundX;
        xPositions[1] = 0f;
        xPositions[2] = -xPositions[0];
    }

    public ObjectPool_LevelObject GetPlatform (int spawnedFloors)
    {
        return RandomlySelectPlatformAmongWeightedOptions(platforms, spawnedFloors);
    }

    public float GetXPos ()
    {
        return GetXPos_Random();
    }

    public void DespawnAll()
    {
        foreach (var p in platforms)
            p.DespawnAll();

    }

    //In threePointSpawn where xPosition indexes are (0, 1, 2), 0 is left, 1 is right.
    float GetXPos_Random() => xPositions[Random.Range(0, xPositions.Length)];
    float GetXPos_Left() => xPositions[0];
    float GetXPos_Mid() => xPositions[1];
    float GetXPos_Right() => xPositions[xPositions.Length - 1];
    float GetXPos_NonCenter() => (Random.Range(0, 2) == 1) ? GetXPos_Left() : GetXPos_Right();
}