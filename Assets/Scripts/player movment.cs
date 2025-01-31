using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] public float moveSpeed = 5f; // Default move speed
    [SerializeField] public float jumpHeight = 5f; // Default jump height
    [SerializeField] public float climbSpeed = 5.5f;
    [SerializeField] public Rigidbody2D rb;
    public Vector2 climbDirection;
    public bool onLadder = false;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); // Cache the Rigidbody2D component
        climbDirection = new Vector2(0, -1);
    }

    void Update()
    {
        // Jumping
        if (Input.GetKeyDown(KeyCode.Space) && Mathf.Abs(rb.velocity.y) < 0.001f && onLadder == false)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpHeight);
            //transform.position += Vector3.up * jumpHeight;
        }

        // Horizontal movement 
        //Climbing ladder and locking movement in horizontal axes
        if(onLadder == false)
        {
            rb.gravityScale = 1;
            float moveInput = Input.GetAxis("Horizontal");
            rb.velocity = new Vector2(moveInput * moveSpeed, rb.velocity.y);
        }
        else if(onLadder == true && Input.GetKey(KeyCode.LeftAlt))
        {
            onLadder = !onLadder;  
        }

        if(Input.GetKeyDown(KeyCode.W) && onLadder == true)
        {
            rb.gravityScale = 0;
            rb.velocity = new Vector2(0, climbSpeed);
        }
        else if(Input.GetKeyUp(KeyCode.W) && onLadder == true)
        {
            rb.velocity = new Vector2(0, 0);
        }
        else if(Input.GetKeyDown(KeyCode.S) && onLadder == true)
        {
            rb.velocity = new Vector2(0, -climbSpeed);
        }
        else if(Input.GetKeyUp(KeyCode.S) && onLadder == true)
        {
            rb.velocity = new Vector2(0, 0);
        }
        
    }

    public void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.gameObject.tag == "LadderSwitch" && onLadder == false)
        {
            rb.velocity = new Vector2(0, 0);
            rb.gravityScale = 0;
            transform.position = other.transform.position;
            onLadder = !onLadder;
        }
        else if(other.gameObject.tag == "LadderBreaker" && onLadder == true)
        {
            rb.velocity = new Vector2(0, 0);
            transform.position = other.transform.position;
            onLadder = !onLadder;
            rb.gravityScale = 1;
        }
    }


}
