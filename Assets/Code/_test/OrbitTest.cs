using UnityEngine;
using System.Collections;

public class OrbitTest : MonoBehaviour
{
    const float RotationSpeed = 1f;
    const float Radius = 2f;

    [SerializeField] Transform centerObject;

    //Status
    float currentAngle;
    int facingModifier = 1;
    Vector2 dirToCenter;

    void Start()
    {
        UpdateDirectionToCenter();
        FindFacing();
        SetOrbitPosition();
        SetOrbitRotation();

        //FindPlayerOrbitAngleAndFacing();
    }

    void Update()
    {
        RotatePlayer();
        //FindPlayerOrbitAngleAndFacing();
    }

    void OnGUI()
    {
        GUI.Label(new Rect(20, 20, 600, 20), "currentAngle: " + currentAngle);
    }

    void RotatePlayer()
    {
        dirToCenter = Quaternion.Euler(0f, 0f, RotationSpeed * facingModifier) * dirToCenter;
        SetOrbitPosition();
        SetOrbitRotation();
    }

    void UpdateDirectionToCenter ()
    {
        dirToCenter = (centerObject.position - transform.position).normalized;
    }

    void SetOrbitRotation ()
    {
        transform.rotation = Quaternion.LookRotation(Vector3.forward, dirToCenter)  * Quaternion.Euler(0f, 0f, facingModifier * -90f);
    }

    void SetOrbitPosition ()
    {
        transform.position = (Vector2)centerObject.position + (-dirToCenter * Radius);
    }

    void FindFacing ()
    {
        Vector3 cross = Vector3.Cross(dirToCenter, transform.up);
        facingModifier = cross.z > 0f ? -1 : 1;
    }
}