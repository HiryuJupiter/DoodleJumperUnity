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
    float CharacterPosY => character.position.y;

    //Getter method
    Vector2 GetSpawnPoint(float posY) => new Vector2(Random.Range(xBoundLeft, xBoundRight), posY);

    void Awake()
    {
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
        if (cam.TopOfScreen > nextSpawnPosY)
        {
            nextSpawnPosY += RandomInterval;
            SpawnAtPosition(GetSpawnPoint(nextSpawnPosY));
        }
    }

    public void RemovePlatform(GameObject p)
    {
        active.Remove(p);
        p.SetActive(false);
        inactive.Add(p);
    }

    //void GameStartInitialGeneration ()
    //{
    //    //Fill the screen with platforms between the character and top of the screen
    //    float y = cam.BottomOfScreen;
    //    float top = cam.TopOfScreen;
    //    while (y < top)
    //    {
    //        y += RandomInterval;
    //        SpawnAtPosition(RandomSpawnPoint(y));
    //    }

    //    //Set the next spawn Y position
    //    nextSpawnPosY = y + RandomInterval;
    //}

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