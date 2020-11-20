using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlatformGenerator : MonoBehaviour
{
    public static PlatformGenerator Instance;

    [Header("World space positional specifications")]
    [SerializeField] float xBoundLeft;
    [SerializeField] float xBoundRight;

    [SerializeField] float spawnIntervalMin = 2f;
    [SerializeField] float spawnIntervalMax = 6f;
    [SerializeField] GameObject pf_platform;

    //Status
    float nextSpawnPosY;
    List<GameObject> active = new List<GameObject>();
    List<GameObject> inactive = new List<GameObject>();

    //Reference
    Transform character;
    CameraTracker cam;

    //Properties
    float RandomInterval => Random.Range(spawnIntervalMin, spawnIntervalMax);

    //Getter method
    Vector2 GetSpawnPoint(float posY) => new Vector2(Random.Range(xBoundLeft, xBoundRight), posY);

    void Awake()
    {
        //Lazy singleton
        Instance = this;
    }

    void Start()
    {
        //Reference
        character = CharacterController.Instance.transform;
        cam = CameraTracker.Instance;

        //Initialize
        nextSpawnPosY = cam.BottomOfScreen;
    }

    void Update()
    {
        //When top of screen is above the next supposed platform spawn point, spawn a 
        //... new platform and increment the next platform spawn point.
        if (cam.TopOfScreen > nextSpawnPosY)
        {
            nextSpawnPosY += RandomInterval;
            SpawnAtPosition(GetSpawnPoint(nextSpawnPosY));
        }
    }

    public void RemovePlatform(GameObject p)
    {
        //Move the platform from the active list to the inactive list and hide it.
        active.Remove(p);
        p.SetActive(false);
        inactive.Add(p);
    }

    void SpawnAtPosition(Vector2 pos)
    {
        if (inactive.Count > 0)
        {
            //If object pool is not empty, then take an object from the pool and make it active
            GameObject p = inactive[0];
            p.SetActive(true);
            p.transform.position = pos;
            inactive.RemoveAt(0);
        }
        else
        {
            //If object pool is empty, then spawn a new object.
            GameObject p = Instantiate(pf_platform, pos, Quaternion.identity, transform);
            active.Add(p);
        }
    }

}