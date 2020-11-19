﻿using UnityEngine;
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
        transform.localScale = faceRight ? scaleFaceRight : scaleFaceLeft;
    }

    public void Lands ()
    {
        StartCoroutine(PlayLandAnimation());
    }

    IEnumerator PlayLandAnimation ()
    {
        sr.sprite = squatPose;
        yield return new WaitForSeconds(0.2f);
        sr.sprite = normalPose;
    }
}