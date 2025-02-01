using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGlide1 : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator anim;

    [Header("Glide Settings")]
    public string glideButton = "Glide"; // Assign a different button for Glide
    public float glideForce = 5f; // Adjust to control glide strength
    public float minGlideHeight = 3f; // Minimum fall distance before gliding is allowed

    private float lastGroundedY; // Stores the last grounded Y position
    private bool canGlide = false; // Ensures gliding activates only when conditions are met
    private bool jumpedFromGround = false; // Tracks if the player jumped from the ground

    [Header("Ground Check")]
    public Transform groundCheck;
    public LayerMask groundLayer;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        CheckGroundStatus();
        HandleGlide();
    }

    private void CheckGroundStatus()
    {
        bool isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);

        if (isGrounded)
        {
            lastGroundedY = transform.position.y; // Save Y position when last on the ground
            canGlide = true; // Reset glide ability when touching the ground
            jumpedFromGround = false; // Reset jump tracking when grounded
        }
    }

    public void NotifyJumpedFromGround()
    {
        jumpedFromGround = true; // Mark that the player jumped from the ground
    }

    private void HandleGlide()
    {
        float fallDistance = lastGroundedY - transform.position.y; // Calculate fall distance

        if (canGlide && !jumpedFromGround && Input.GetButton(glideButton) && rb.velocity.y < 0 && transform.position.y > 2.0)// for changing Glide Button go to Input manager in settings
        {
            rb.AddForce(Vector2.up * glideForce, ForceMode2D.Force); // Apply upward force to slow fall
            anim.SetBool("isGliding", true);
        }
        else
        {
            anim.SetBool("isGliding", false);
        }

        // Disable gliding after first activation
       // if (anim.GetBool("isGliding"))
       // {
       //     canGlide = false;
       // }
    }
}
