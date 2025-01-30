using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlideScript : MonoBehaviour
{
    public float glideGravity = 0.5f; // Reduced gravity when gliding
    private float normalGravity; // Default gravity
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        normalGravity = rb.gravityScale; // Store the original gravity
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.LeftShift) && rb.velocity.y < 0)
        {
            rb.gravityScale = glideGravity; // Reduce gravity while falling
        }
        else
        {
            rb.gravityScale = normalGravity; // Restore gravity when not gliding
        }
    }


}
