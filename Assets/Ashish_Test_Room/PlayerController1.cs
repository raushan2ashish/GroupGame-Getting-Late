using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController1 : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 10f;
    private Rigidbody2D rb;
    private Animator anim;
    private bool isFacingRight = true;
    private bool isGrounded;
    public Transform groundCheck;
    public LayerMask whatIsGround;


    private PlayerGlide1 playerGlide; // Reference to Glide Script



    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        playerGlide = GetComponent<PlayerGlide1>(); // Get reference to PlayerGlide script
    }

    private void Update()
    {
        // Horizontal movement
        float moveInput = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(moveInput * moveSpeed, rb.velocity.y);
        anim.SetFloat("Speed", Mathf.Abs(moveInput));

        if (isFacingRight == false && moveInput > 0)
        {
            Flip();
        }
        else if (isFacingRight == true && moveInput < 0)
        {
            Flip();
        }
        Jump();
        // Jumping
        // if (isGrounded && Input.GetKeyDown(KeyCode.Space))
        // {
        //     rb.velocity = Vector2.up * jumpForce;
        //     //anim.SetTrigger("Jump");
        // }

        
    }
    private void Jump()
    {
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            //anim.SetTrigger("Jump");

            if (playerGlide != null)
            {
                playerGlide.NotifyJumpedFromGround(); // Inform Glide script that jump started from the ground
            }
        }
    }

    private void FixedUpdate()
    {
        // Ground check
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.2f, whatIsGround);
        anim.SetBool("isGrounded", isGrounded);
    }

    private void Flip()
    {
        isFacingRight = !isFacingRight;
        Vector3 scaler = transform.localScale;
        scaler.x *= -1;
        transform.localScale = scaler;
    }



}
