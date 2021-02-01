using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(LevelSpawnManager))]
public class MainZoneSubSpawner : SubSpawner
{
    const float SpawnBoundX = 2f;
    const float BumperXPosition = 2.5f;
    const int CenterXPosIndex = 2;
    const int PercentageChance_BumperSpawn = 2;

    [Header("Platforms")]
    [SerializeField] ObjectPool_Platform basicPlatform;
    [SerializeField] ObjectPool_Platform extraHighPlatform;
    [SerializeField] ObjectPool_Platform starZoomPlatform;
    [SerializeField] ObjectPool_Platform rainbowPlatform;
    [SerializeField] ObjectPool_Platform singleJumpPlatform;

    [SerializeField] List<ObjectPool_Platform> platforms;

    [Header("Bumper")]
    [SerializeField] ObjectPool_LevelObject bumperLPlatform;
    [SerializeField] ObjectPool_LevelObject bumperRPlatform;

    [Header("Spinner")]
    [SerializeField] ObjectPool_LevelObject spinner;

    [Header("Translator")]
    [SerializeField] ObjectPool_LevelObject linearTranslator;

    //Ref

    //Status
    bool nextBumperRight;

    //Spawn positions
    float[] xPositions = new float[5];

    protected override void Awake()
    {
        base.Awake();
        //Ref

        //Initialize platform list
        platforms = new List<ObjectPool_Platform>
        {
            basicPlatform,
            extraHighPlatform,
            starZoomPlatform,
            rainbowPlatform,
            singleJumpPlatform,
        };

        // Initialize spawning positions
        xPositions[0] = -SpawnBoundX;
        xPositions[1] = -SpawnBoundX / 2f;
        xPositions[2] = 0f;
        xPositions[3] = SpawnBoundX / 2f;
        xPositions[4] = SpawnBoundX;
    }


    #region Public
    public void Spawn()
    {
        if (Random.Range(0, 100) < PercentageChance_BumperSpawn)
        {
            SpawnBumper();
        }
        else
        {
            SpawnPlatform();
        }
    }
    #endregion

    #region Spawn bumper
    public void SpawnBumper()
    {
        for (int i = 0; i < Random.Range(2, 9); i++)
        {
            ObjectPool_LevelObject bumper = (nextBumperRight = !nextBumperRight) ? bumperRPlatform : bumperLPlatform;

            bumper.Spawn(GetNextBumperSpawnLocation());
            status.GetNextSpawnPosY += bumper.ObjectYSize;

            //GM.SpawnedFloors++;
        }
    }
    #endregion

    #region SpawnPlatform
    void SpawnPlatform(SpawningStatus status)
    {
        GetRandomPlatform(status.SpawnedFloors).Spawn(new Vector2(GetXPos_Random(), status.NextSpawnPosY));
        //GM.SpawnedFloors++;
    }

    ObjectPool_LevelObject GetRandomPlatform(int spawnedFloors) => RandomlySelectPlatformAmongWeightedOptions(platforms, spawnedFloors);


    // GET POSITIONS
    float GetXPos_Random() => xPositions[Random.Range(0, xPositions.Length)];
    float GetXPos_Left() => xPositions[0];
    float GetXPos_Mid() => xPositions[CenterXPosIndex];
    float GetXPos_Right() => xPositions[xPositions.Length - 1];

    float GetXPos_NonCenter()
    {
        int pointIndex;
        do
        {
            pointIndex = Random.Range(0, xPositions.Length);
        }
        while (pointIndex != CenterXPosIndex);

        return xPositions[pointIndex];
    }
    #endregion

    #region Minor methods
    //DESPAWN 
    public void DespawnAll()
    {
        foreach (var p in platforms)
            p.DespawnAll();

        bumperLPlatform.DespawnAll();
        bumperRPlatform.DespawnAll();
        spinner.DespawnAll();
        linearTranslator.DespawnAll();
    }

    void RandomizeNextBumperLocation() => nextBumperRight = Random.Range(0, 2) == 0 ? true : false;

    Vector2 GetNextBumperSpawnLocation() => new Vector2((nextBumperRight ? BumperXPosition : -BumperXPosition), nextSpawnPosY);
    #endregion
}