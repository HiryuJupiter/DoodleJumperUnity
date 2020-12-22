using UnityEngine;
using System.Collections;

public class PlayerFeedbacks : MonoBehaviour
{
    [SerializeField] Sprite normalPose;
    [SerializeField] Sprite squatPose;
    SpriteRenderer sr;

    //Cache
    Vector3 scaleFaceRight;
    Vector3 scaleFaceLeft;

    void Awake()
    {
        //Reference
        sr = GetComponent<SpriteRenderer>();

        //Cache
        scaleFaceRight = transform.localScale;
        scaleFaceLeft = scaleFaceRight;
        scaleFaceLeft.x = -scaleFaceLeft.x;
    }

    public void SetFacing (bool faceRight)
    {
        transform.localScale = faceRight ? scaleFaceRight : scaleFaceLeft;
    }

    public void Play_LandsOnPlatform()
    {
    }
}