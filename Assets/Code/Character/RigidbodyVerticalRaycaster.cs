using UnityEngine;
using System;
using System.Collections;

[RequireComponent(typeof(Collider2D))]
public class RigidbodyVerticalRaycaster : MonoBehaviour
{
    //Ref
    Rigidbody2D rb;

    //Cache
    Vector2 offset_BL;
    Vector2 offset_BR;
    float colliderYExtent;
    Action<Collider2D> hitsColliderCallback;
    LayerMask interactableLayer;

    public Vector2 BL { get; private set; }
    public Vector2 BR { get; private set; }

    #region Mono
    void Awake()
    {
        //Initialize
        Bounds bounds = GetComponent<Collider2D>().bounds;
        float x = bounds.extents.x - 0.005f;
        float y = bounds.extents.y - 0.005f;
        offset_BL = new Vector2(-x, -y);
        offset_BR = new Vector2(x, -y);

        colliderYExtent = bounds.extents.y;
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        interactableLayer = GameSettings.Instance.InteractableLayer;
    }
    #endregion

    #region Raycast hit
    public void CheckForCollidersBeneath(Action<Collider2D> hitsColliderCallback)
    {
        if (isFalling)
        {
            UpdateOriginPoints();
            this.hitsColliderCallback = hitsColliderCallback;

            if (!VerticalRaycast(BL))
            {
                VerticalRaycast(BR);
            }
        }
    }

    bool VerticalRaycast(Vector2 origin)
    {
        RaycastHit2D hit = Physics2D.Raycast(origin, Vector2.up, rb.velocity.y * Time.deltaTime, interactableLayer);
        Debug.DrawRay(origin, Vector2.up * rb.velocity.y * Time.deltaTime, Color.cyan);
        if (hit.collider != null)
        {
            MoveTransformToHitSurface(hit.point);
            hitsColliderCallback(hit.collider);
            return true;
        }
        return false;
    }
    #endregion

    #region Minor methods
    void MoveTransformToHitSurface (Vector2 hitPoint)
    {
        //Position the player right above the raycastHit object
        float bottomPoint = transform.position.y - colliderYExtent;
        float distToHitSurface = bottomPoint - hitPoint.y;

        Vector2 bottom = new Vector2(transform.position.x, bottomPoint);

        Debug.DrawRay(bottom, Vector3.right, Color.red, 10f);
        Debug.DrawRay(hitPoint, Vector3.right, Color.yellow, 10f);

        //Move the character right up against the platform's surface, so that we have consistent jumps.
        Vector3 p = transform.position;
        p.y -= distToHitSurface;
        transform.position = p;

    }
    void UpdateOriginPoints()
    {
        BL = (Vector2)transform.position + offset_BL;
        BR = (Vector2)transform.position + offset_BR;

        Debug.DrawRay(BL, Vector3.left, Color.blue);
        Debug.DrawRay(BR, Vector3.right, Color.blue);
    }

    bool isFalling => rb.velocity.y < 0f;
    #endregion
}