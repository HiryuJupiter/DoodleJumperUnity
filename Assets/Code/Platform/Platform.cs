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
        if (transform.position.y < cam.BottomOfScreen)
        {
            Destroy();
        }
    }

    public void Destroy ()
    {
        //Remove platform  and return it to object pool
        pool.RemovePlatform(gameObject);
    }
}