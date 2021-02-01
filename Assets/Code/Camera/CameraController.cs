using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    Animator animator;
    int anim_zoomIn;
    int anim_zoomOut;

    void Start()
    {
        anim_zoomIn = Animator.StringToHash("ZoomIn");
        anim_zoomOut = Animator.StringToHash("ZoomOut");

        animator = GetComponent<Animator>();

        //Events
        GameManager.OnGameStart += ZoomOut;
        GameManager.OnScoreboardBackToMain += ZoomIn;

    }

    void OnDisable()
    {
        GameManager.OnGameStart -= ZoomOut;
        GameManager.OnScoreboardBackToMain -= ZoomIn;
    }


    void ZoomOut()
    {
        animator.Play(anim_zoomOut);
    }

    void ZoomIn ()
    {
        animator.Play(anim_zoomIn);
    }
}