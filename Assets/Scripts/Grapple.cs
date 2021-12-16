using System;
using UnityEngine;

public class Grapple : MonoBehaviour
{
    private LineRenderer lineRenderer;
    private Vector2 grapplePoint;
    public LayerMask grapplableLayerMask;
    public Transform player;
    public float maxDistance = 100f;
    private SpringJoint2D joint;
    private Vector2 currentGrapplePosition;
    private bool readyToGrapple = true;
    public float grappleCooldown = 2f;
    private void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && readyToGrapple)
        {
            StartGrapple();
        }

        if (Input.GetMouseButtonUp(0))
        {
            StopGrapple();
        }
    }

    private void LateUpdate()
    {
        DrawRope();
    }

    void StartGrapple()
    {
        readyToGrapple = false;
        
        Vector3 position = player.position;
        RaycastHit2D hit =
            Physics2D.Raycast(position, Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position,
                maxDistance, grapplableLayerMask);
        if (hit.collider != null)
        {
            grapplePoint = hit.point;
            joint = player.gameObject.AddComponent<SpringJoint2D>();
            joint.autoConfigureConnectedAnchor = false;
            joint.connectedAnchor = grapplePoint;

            float distanceFromPoint = Vector2.Distance(player.position, grapplePoint);

            joint.distance = distanceFromPoint * 0.8f;
            joint.dampingRatio = 0.4f;

            lineRenderer.positionCount = 2;
            currentGrapplePosition = position;
        }
        
        Invoke(nameof(ResetGrapple), grappleCooldown);
    }

    void StopGrapple()
    {
        lineRenderer.positionCount = 0;
        if (GetComponents<SpringJoint2D>().Length > 1)
        {
            foreach (SpringJoint2D component in GetComponents<SpringJoint2D>())
            {
                Destroy(component);
            }
        }
        else
        {
            Destroy(joint);
        }
    }
    
    void ResetGrapple()
    {
        readyToGrapple = true;
    }

    void DrawRope()
    {
        if (!joint) return;

        currentGrapplePosition = Vector2.Lerp(currentGrapplePosition, grapplePoint, Time.deltaTime * 8f);

        lineRenderer.SetPosition(0, player.position);
        lineRenderer.SetPosition(1, currentGrapplePosition);
    }

    public bool IsGrappling()
    {
        return joint != null;
    }

    public Vector2 GetGrapplePoint()
    {
        return grapplePoint;
    }
}