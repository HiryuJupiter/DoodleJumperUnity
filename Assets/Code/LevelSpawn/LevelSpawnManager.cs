using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/* 
Five point - is referring to, the spawn locations are divided into 5 locations along the x-axis.
0, 1, 2, 3, 4; 2 is center, 0 is left, 4 is right. 
To make the code less verbose, I am sacrificing robustness by hard coding these magical numbers
 */
[RequireComponent(typeof(MainZoneSubSpawner))]
[RequireComponent(typeof(StartingZoneSubSpawner))]

public class LevelSpawnManager : MonoBehaviour
{
    public delegate void PlatformSpawnHandler();
    public static event PlatformSpawnHandler OnPlatformSpawned;

    const int FivePoint_XPositionIndex_Center = 2; //In fivePointSpawn where xPosition  indexes are (0, 1, 2, 3, 4), 2 is center
    const int FloorRange_StartingZone = 30;

    [Header("Spawn settings")]
    [SerializeField] float spawnInterval = 3f;
    [SerializeField] float bumperXPosition = 2.5f;

    //Reference
    GameManager gm;
    GameSettings settings;
    MainZoneSubSpawner mainZoneSpawner;
    StartingZoneSubSpawner startingZoneSpawner;

    //Status
    bool isSpawning;
    public int SpawnedFloors { get; private set; } //The number of platforms spawned
    public float NextSpawnPosY { get; private set; }

    #region Public method
    public void IncrementSpawnnedFloors() => SpawnedFloors++;
    public void IncrementNextSpawnPosY(float platformSize) => NextSpawnPosY = NextSpawnPosY + platformSize + spawnInterval;
    #endregion

    #region MonoBehavior
    void Awake()
    {
        //Ref
        settings = GameSettings.Instance;
        mainZoneSpawner = GetComponent<MainZoneSubSpawner>();
        startingZoneSpawner = GetComponent<StartingZoneSubSpawner>();
    }

    void Start()
    {
        //Reference
        gm = GameManager.Instance;

        //Event 
        GameManager.OnGameStart += StartSpawning;
        GameManager.OnGameOver += StopSpawning;
    }

    void OnDisable()
    {
        GameManager.OnGameStart -= StartSpawning;
        GameManager.OnGameOver -= StopSpawning;
    }

    void OnGUI()
    {
        GUI.Label(new Rect(20, 400, 500, 50), "SpawnedFloors: " + SpawnedFloors, GameSettings.GuiStyle);
        GUI.Label(new Rect(20, 450, 500, 50), "TopOfScreen: " + CameraTracker.TopOfScreen, GameSettings.GuiStyle);
        GUI.Label(new Rect(20, 500, 500, 50), "nextSpawnPosY: " + nextSpawnPosY, GameSettings.GuiStyle);
    }
    #endregion

    #region Spawning logic
    void StartSpawning()
    {
        Reset();
        InitialTestSpawnSequence();
        StartCoroutine(SpawningConditionCheck());
    }

    void InitialTestSpawnSequence()
    {
        //Spawn a platform at the center bottom of the screen to start off. 
        //SpawnPlatform(long_basicPlatform, GetSpawnLocation_ThreePoint_NonCenter());
        //SpawnPlatform(long_basicPlatform, GetSpawnLocation_ThreePoint_Left());
    }

    bool InStartingZone() => SpawnedFloors < FloorRange_StartingZone;

    IEnumerator SpawningConditionCheck()
    {
        spawningStatus.IsSpawning = true;

        while (spawningStatus.IsSpawning)
        {
            if (CameraAboveSpawnPoint())
            {
                if (InStartingZone())
                {
                    
                    SpawnStartingZonePlatform();
                }
                else
                {
                    SpawnMainZonePlatform();
                }
            }
            yield return null;
        }
    }

    void SpawnStartingZonePlatform()
    {
        startingZoneSpawner.Spawn
    }

    void SpawnMainZonePlatform()
    {

    }

    void StopSpawning()
    {
        startingZoneSpawner.DespawnAll();
        mainZoneSpawner.DespawnAll();
        spawningStatus.IsSpawning = false;
    }
    #endregion

    #region MinorMethods
    bool CameraAboveSpawnPoint() => CameraTracker.TopOfScreen > spawningStatus.NextSpawnPosY;
    
    void Reset()
    {
        spawningStatus.NextSpawnPosY = CameraTracker.BottomOfScreen + spawnInterval * .5f;
        SpawnedFloors = 0;
    }

    Vector3 GetSpawnLocation(float xPos) => new Vector2(xPos, spawningStatus.NextSpawnPosY);
    void IncreaseNextSpawnPosY(float spawnedObjectSize)
    {
        spawningStatus.NextSpawnPosY += spawnInterval + spawnedObjectSize;
    }
    #endregion
}

/*
 
    void SpawnRandomThreePointPlatform() => SpawnPlatform(entryZoneSpawner.GetPlatform(),
        entryZoneSpawner.GetSpawnLocation());
    void SpawnRandomFivePointPlatform() => SpawnPlatform(SelectRandomPlatformFromWeightedCollection(short_platforms), GetSpawnLocation_FivePoint_Random());


    void SpawnBumper()
    {

        Debug.Log("Start spawning bumper :");
        //Spawn 2~8 bumpers
        for (int i = 0; i < Random.Range(2, 9); i++)
        {
            ObjectPool_LevelObject bumper = (nextBumperRight = !nextBumperRight) ? bumperRPlatform : bumperLPlatform;

            bumper.Spawn(GetNextBumperSpawnLocation());
            IncreaseNextSpawnPosY(bumper.ObjectYSize);

            SpawnedFloors++;
        }
    }

    void SpawnPlatform(ObjectPool_LevelObject platform, Vector2 spawnPos)
    {
        platform.Spawn(spawnPos);

        IncreaseNextSpawnPosY(platform.ObjectYSize);
        SpawnedFloors++;
    }

    void StopSpawning()
    {
        entryZoneSpawner.DespawnAll();
        mainZoneSpawner.DespawnAll();

        bumperLPlatform.DespawnAll();
        bumperRPlatform.DespawnAll();

        spawning = false;
    }
    #endregion

    #region MinorMethods
    bool CameraAboveSpawnPoint() => CameraTracker.TopOfScreen > nextSpawnPosY;

    
    void Reset()
    {
        nextSpawnPosY = CameraTracker.BottomOfScreen + spawnInterval * .5f;
        SpawnedFloors = 0;
    }

    Vector3 GetSpawnLocation(float xPos) => new Vector2(xPos, nextSpawnPosY);
    void IncreaseNextSpawnPosY(float spawnedObjectSize)
    {
        nextSpawnPosY += spawnInterval + spawnedObjectSize;
    }
    #endregion
 */
/*
     public void RemovePlatform(GameObject p)
    {
        active.Remove(p);
        p.SetActive(false);
        inactive.Add(p);
    }

    void SpawnAtPosition(Vector2 pos)
    {
        if (inactive.Count > 0)
        {
            GameObject p = inactive[0];
            p.SetActive(true);
            p.transform.position = pos;
            inactive.RemoveAt(0);
        }
        else
        {
            GameObject p = Instantiate(pf_platform, pos, Quaternion.identity, transform);
            active.Add(p);
        }
    }
 */