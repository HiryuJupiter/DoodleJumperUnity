using UnityEngine;
using System.Collections;

public static class OrthographicCameraUtil
{
    public static float GetScreenHeight => Camera.main.orthographicSize * 2f;
    public static float GetScreenWidth => GetScreenHeight * Camera.main.aspect;
}