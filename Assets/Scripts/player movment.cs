using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] public float moveSpeed = 5f; // Default move speed
    [SerializeField] public float jumpHeight = 5f; // Default jump height
    [SerializeField] public float climbSpeed = 5.5f;
    [SerializeField] public float openUmbrellaModifier = -1.0f;
    [SerializeField] public float openUmbHoriModifier = 0.5f;
    [SerializeField] public Rigidbody2D rb;
    public Umbrella umbrella;
    public Vector2 climbDirection;
    public bool onLadder;
    public bool isShielding;
    public float rigidBodyVelocityY;
    private bool isFacingRight = true; //for Chacter Flip

    private Animator anim; //for animation
    
    
    public void Start()
    {
        rb = GetComponent<Rigidbody2D>(); // Cache the Rigidbody2D component
        climbDirection = new Vector2(0, -1);
        onLadder = false;
        isShielding = false;
        rigidBodyVelocityY = rb.velocity.y;

        anim = GetComponent<Animator>();
    }

    public void Update()
    {

        // Jumping
        if (Input.GetKeyDown(KeyCode.Space) && Mathf.Abs(rb.velocity.y) < 0.001f && onLadder == false)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpHeight);
            //transform.position += Vector3.up * jumpHeight;

        }

        //Jump Animation when character Verticle Speed is more than 0
        if (Mathf.Abs(rb.velocity.y) > 0.001f && isShielding == false)
        {
            anim.SetBool("isJumping", true);//Jump Animation Enabled
        }
        else if(Mathf.Abs(rb.velocity.y) < 0.001f && isShielding == false)
        {
            anim.SetBool("isJumping", false);//Jump Animation Desabled
        }
            


        // Horizontal movement 
        //Climbing ladder and locking movement in horizontal axes
        //Gliding mid-air
        if (onLadder == false && isShielding == false)
        {
            rb.gravityScale = 1;
            float moveInput = Input.GetAxis("Horizontal");
            rb.velocity = new Vector2(moveInput * moveSpeed, rb.velocity.y);
            anim.SetFloat("Speed", Mathf.Abs(moveInput));

            if (isFacingRight == false && moveInput > 0 && onLadder == false)
            {
                Flip();
            }
            else if (isFacingRight == true && onLadder == false && moveInput < 0)
            {
                Flip();
            }

        }
        else if (onLadder == false && isShielding == true && rb.velocity.y > -1.0f)
        {
            rb.gravityScale = 1;
            float moveInput = Input.GetAxis("Horizontal");
            rb.velocity = new Vector2(moveInput * (moveSpeed * openUmbHoriModifier), rb.velocity.y);
            
        }
        else if(onLadder == false && isShielding == true && rb.velocity.y <= -1.0f)
        {
            rb.gravityScale = 1;
            float moveInput = Input.GetAxis("Horizontal");
            rb.velocity = new Vector2(moveInput * (moveSpeed * openUmbHoriModifier), openUmbrellaModifier);
            
       
        }
        else if(onLadder == true && Input.GetKey(KeyCode.LeftAlt))
        {
            onLadder = !onLadder;

            
        }

        // Ensure character faces right when on ladder
        if (onLadder == true && isFacingRight == false)
        {
            FaceRight();
        }

        if (Input.GetKeyDown(KeyCode.W) && onLadder == true)
        {
            rb.gravityScale = 0;
            rb.velocity = new Vector2(0, climbSpeed);
            anim.SetBool("isClimbing", true);//Ladder Climb Animation enable
        }
        else if (Input.GetKeyUp(KeyCode.W) && onLadder == true)
        {
            rb.velocity = new Vector2(0, 0);
            anim.SetBool("isClimbing", false);//Ladder Climb Animation desable
        }
        else if (Input.GetKeyDown(KeyCode.S) && onLadder == true)
        {
            rb.velocity = new Vector2(0, -climbSpeed);
            anim.SetBool("isClimbing", true);//Ladder Climb Animation enable
        }
        else if (Input.GetKeyUp(KeyCode.S) && onLadder == true)
        {
            rb.velocity = new Vector2(0, 0);
            anim.SetBool("isClimbing", false);//Ladder Climb Animation desable
        }
        
        


        //Access umbrella opening and closing
        if (Input.GetKeyDown(KeyCode.C) && onLadder == false)
        {
            isShielding = !isShielding;
            umbrella.ShieldSwitch();
            
        }

        //Auto close umbrella when on ladder
        if(onLadder == true && isShielding == true)
        {
            isShielding = !isShielding;
            umbrella.ShieldSwitch();
        }   
    }

    //Automatically starts or ends ladder moveset
    public void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.gameObject.tag == "LadderSwitch" && onLadder == false)
        {
            rb.velocity = new Vector2(0, 0);
            rb.gravityScale = 0;
            transform.position = other.transform.position;
            onLadder = !onLadder;
            anim.SetBool("isOnLadder", true);//Ladder Climb Animation enable

        }
        else if(other.gameObject.tag == "LadderBreaker" && onLadder == true)
        {
            rb.velocity = new Vector2(0, 0);
            transform.position = other.transform.position;
            onLadder = !onLadder;
            rb.gravityScale = 1;
            anim.SetBool("isOnLadder", false);
            anim.SetBool("isClimbing", false);//Ladder Climb Animation desable
        }
    }

    public void OnCollisionEnter2D(Collision2D other) 
    {
        if(other.gameObject.tag == "Hostile")
        {
            Debug.Log("Player Took Damage");
        }
    }

    private void Flip()
    {
        isFacingRight = !isFacingRight;
        Vector3 scaler = transform.localScale;
        scaler.x *= -1;
        transform.localScale = scaler;
    }

    // FaceRight method to ensure the character faces right
    private void FaceRight()
    {
        if (!isFacingRight)
        {
            isFacingRight = true;
            Vector3 scaler = transform.localScale;
            scaler.x *= -1;
            transform.localScale = scaler;
        }
    }

    public void Ice()
    {
        moveSpeed = moveSpeed + 2;
    }
    public void mud()
    {
        moveSpeed = moveSpeed -2;
    }
    public void normal()
    {
        if(moveSpeed != 5.0f)
        {
            moveSpeed = 5.0f;
        }
    }
}
