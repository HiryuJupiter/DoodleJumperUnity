using UnityEngine;
using System.Collections;

public class Platform : MonoBehaviour
{
    PlatformGenerator pool;
    CameraTracker cam;

    void Awake()
    {
        pool = PlatformGenerator.Instance;
        cam = CameraTracker.Instance;
    }

    void Update()
    {
        //When platform goes below the camera's bottom edge, recycle this
        if (transform.position.y < cam.BottomOfScreen)
        {
            Destroy();
        }
    }

    //This is technically "recycle" but I call it Destroy because it's easier to understand as a wrapper name
    public void Destroy ()
    {
        //Remove platform  and return it to object pool
        pool.RemovePlatform(gameObject);
    }
}