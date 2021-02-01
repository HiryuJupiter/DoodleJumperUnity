using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerFeedbacks : MonoBehaviour
{
    [SerializeField] Sprite normalPose;
    [SerializeField] Sprite squatPose;

    [SerializeField] Image bannerBlue;
    [SerializeField] Image bannerYellow;
    [SerializeField] Image bannerRed;

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

    public void SetCharacterVisibility (bool isTrue)
    {
        sr.enabled = isTrue;
    }

    public void EnterMode_ExtraHigh() => StartCoroutine(DoEnterMode_ExtraHigh());
    public void EnterMode_StarZoom() => StartCoroutine(DoEnterMode_StarZoom());
    public void EnterMode_Rainbow() => StartCoroutine(DoEnterMode_Rainbow());


    IEnumerator DoEnterMode_ExtraHigh()
    {
        bannerBlue.enabled = true;
        yield return 0.1f;
        bannerBlue.enabled = false;
    }
    IEnumerator DoEnterMode_StarZoom()
    {
        bannerYellow.enabled = true;
        yield return 0.5f;
        bannerYellow.enabled = false;
    }

    IEnumerator DoEnterMode_Rainbow()
    {
        bannerRed.enabled = true;
        yield return 1f;
        bannerRed.enabled = false;
    }
}