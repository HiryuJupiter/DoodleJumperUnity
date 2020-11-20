using UnityEngine;
using System.Collections;

public class CharacterAnimationController : MonoBehaviour
{
    [SerializeField] Sprite normalPose;
    [SerializeField] Sprite squatPose;
    SpriteRenderer sr;
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
        //Set facing by flipping the scale
        transform.localScale = faceRight ? scaleFaceRight : scaleFaceLeft;
    }

    public void PlayLanding ()
    {
        //A simple coroutine for basic jumping animation
        StartCoroutine(PlayLandAnimation());
    }

    IEnumerator PlayLandAnimation ()
    {
        //Give the character a squat pose and then switch back to jump pose
        sr.sprite = squatPose;
        yield return new WaitForSeconds(0.2f);
        sr.sprite = normalPose;
    }
}