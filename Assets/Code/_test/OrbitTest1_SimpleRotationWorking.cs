using UnityEngine;
using System.Collections;

public class OrbitTest1_SimpleRotationWorking : MonoBehaviour
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
        dirToCenter = Quaternion.Euler(0f, 0f, RotationSpeed) * dirToCenter;
        SetOrbitPosition();
        SetOrbitRotation();
    }

    void FindPlayerOrbitAngleAndFacing()
    {
        Vector2 dirFromCenter = transform.position - centerObject.position;
        currentAngle = Vector2.Angle(Vector3.up, dirFromCenter);

        //Make orbit object 
        Vector2 dirToCenter = centerObject.position - transform.position;
        transform.localRotation = Quaternion.LookRotation(Vector3.forward, dirToCenter);


        Debug.DrawRay(centerObject.position, dirFromCenter, Color.red);
        Debug.DrawRay(transform.position, transform.up, Color.yellow);
        //Vector3.Cross(dirToSpinner, player.transform.up);
    }

    void UpdateDirectionToCenter ()
    {
        dirToCenter = (centerObject.position - transform.position).normalized;
    }

    void SetOrbitRotation ()
    {
        transform.rotation = Quaternion.LookRotation(Vector3.forward, dirToCenter);
    }

    void SetOrbitPosition ()
    {
        transform.position = (Vector2)centerObject.position + (-dirToCenter * Radius);
    }
}

/*
 * 
        currentAngle += RotationSpeed * facingModifier * Time.deltaTime;
        transform.localRotation = Quaternion.Euler(0f, 0f, currentAngle);

         Vector2 dirFromCenter = transform.position - centerObject.position;
        currentAngle = Vector2.Angle(Vector3.up, dirFromCenter);
        //Vector3.Cross(dirToSpinner, player.transform.up);
 */