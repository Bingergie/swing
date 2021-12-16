using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Time
    public float timeScale;
    
    // Movement
    public float moveSpeed;
    public float maxSpeed;
    public float jumpForce;
    
    private float threshold = 0.01f;
    public LayerMask groundLayerMask;
    
    // Jumping
    public float jumpCooldown = 2f;
    public bool readyToJump = true;
    public float counterMovement;
    
    // Grappling
    public GameObject grappleObject;

    // Input
    float xDirection;

    private Rigidbody2D _rigidbody;
    private Collider2D _collider;
    private Grapple _grapple;

    void Awake()
    {
        Time.timeScale = timeScale;
        _rigidbody = GetComponent<Rigidbody2D>();
        _collider = GetComponent<Collider2D>();
        _grapple = grappleObject.GetComponent<Grapple>();
    }

    void FixedUpdate()
    {
        // Jumping
        if (Input.GetButton("Jump"))
        {
            Jump();
        }

        if (!_grapple.IsGrappling())
        {
            // Movement
            float xMag = _rigidbody.velocity.x;
            xDirection = Input.GetAxisRaw("Horizontal");

            // Override xDirection with 0 if player is travelling at max speed
            if (xDirection > 0 && xMag > maxSpeed) xDirection = 0;
            if (xDirection < 0 && xMag < -maxSpeed) xDirection = 0;

            _rigidbody.AddForce(Vector2.right * (xDirection * moveSpeed * Time.deltaTime));

            // Counter movement
            if (IsGrounded())
            {
                if (Math.Abs(xMag) > threshold && Math.Abs(xDirection) < 0.05f ||
                    (xMag < -threshold && xDirection > 0) || (xMag > threshold && xDirection < 0))
                {
                    _rigidbody.AddForce(Vector2.right * (moveSpeed * Time.deltaTime * -xMag * counterMovement));
                }
            }
        }
    }
    
    private void Jump()
    {
        if ((IsGrounded() || _grapple.IsGrappling()) && readyToJump)
        {
            readyToJump = false;
            _rigidbody.AddForce(Vector2.up * jumpForce);
            
            Invoke(nameof(ResetJump), jumpCooldown);
        }
    }
    
    private void ResetJump()
    {
        readyToJump = true;
    }

    private bool IsGrounded()
    {
        return Physics2D.BoxCast(_collider.bounds.center, _collider.bounds.size, 0f, Vector2.down, 0.1f,
            groundLayerMask);
    }
}