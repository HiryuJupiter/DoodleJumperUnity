using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/* 
 We want basic platforms to spawn starting at 100% and then go down to 20%.
All spawn chances are in percentage
 */

public class EnvironmentGenerator : MonoBehaviour
{
    const float MaxChance_BasicPlatform = 100f;
    const float MinChance_BasicPlatform = 20f;

    public static EnvironmentGenerator Instance;

    [Header("World space positional specifications")]
    [SerializeField] float xBoundLeft;
    [SerializeField] float xBoundRight;

    [Header("Spawn timing")]
    [SerializeField] float spawnIntervalMin = 2f;
    [SerializeField] float spawnIntervalMax = 6f;

    [Header("Prefab")]
    [SerializeField] GameObject pf_platform;
    [SerializeField] GameObject pf_rocket;
    [SerializeField] GameObject pf_balloon;
    [SerializeField] GameObject pf_superPlatform;
    [SerializeField] GameObject pf_crackPlatform;
    [SerializeField] GameObject pf_bird_pigeon;
    [SerializeField] GameObject pf_bird_puffy;
    [SerializeField] GameObject pf_bird_duck;

    //Reference
    CameraTracker cam;

    //Status
    float nextSpawnPosY;
    bool spawnedBasicPlatform;
    float chance_BasicPlatform; //Percentage chance 

    public Pool pool_basicPlatform { get; private set; }

    #region MonoBehavior
    void Awake()
    {
        //Lazy singleton
        Instance = this;

        //Initialize
        pool_basicPlatform = new Pool(pf_platform);
    }

    void Start()
    {
        //Reference
        cam = CameraTracker.Instance;

        //Initialize
        nextSpawnPosY = cam.BottomOfScreen;

        //Event subscription
        SceneEvents.Bounced.Event += Bounced;
        SceneEvents.GameStart.Event += Reset;
    }

    void Update()
    {
        if (cam.TopOfScreen > nextSpawnPosY)
        {
            Spawn();
            nextSpawnPosY += RandomInterval();
        }
    }

    void OnDisable()
    {
        SceneEvents.Bounced.Event -= Bounced;
        SceneEvents.GameStart.Event -= Reset;
    }
    #endregion

    #region Public
    void Bounced ()
    {
        chance_BasicPlatform = Mathf.Clamp(
            chance_BasicPlatform, 
            MinChance_BasicPlatform, chance_BasicPlatform - 1);
    }
    #endregion

    #region Private
    void Reset ()
    {
        chance_BasicPlatform = MaxChance_BasicPlatform;
    }

    void Spawn()
    {
        //Make sure that we do not spawn 2 non-platforms in a row, or it'll be too difficult to play.
        if (!spawnedBasicPlatform)
        {
            SpawnBasicPlatform();
        }
        else
        {
            spawnedBasicPlatform = Random.Range(0f, 100f) > chance_BasicPlatform;
            if (spawnedBasicPlatform)
            {
                SpawnBasicPlatform();
            }
            else
            {

            }
        }
        
    }

    void SpawnNonBasicPlatoform()
    {

    }

    void SpawnBasicPlatform () => pool_basicPlatform.Spawn(NextSpawnPoint());
    #endregion

    Vector2 NextSpawnPoint() => new Vector2(Random.Range(xBoundLeft, xBoundRight), nextSpawnPosY);
    float RandomInterval() => Random.Range(spawnIntervalMin, spawnIntervalMax);
}


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