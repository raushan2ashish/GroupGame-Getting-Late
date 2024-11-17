using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pctrl : MonoBehaviour
{
    public float speed = 5.0f;
    public float jumpForce = 7f;
    public float crouchTime = 1.0f;
    public Vector3 startingPosition;
    private bool canDoubleJump = true;
    public bool isCrouching = false;

    public Rigidbody2D rb;
    private CapsuleCollider2D capsuleCollider;
    private Vector2 originalColliderSize;
    private bool isOnGround;
    
    // Start is called before the first frame update
    void Start()
    {
       rb = GetComponent<Rigidbody2D>();
       capsuleCollider = GetComponent<CapsuleCollider2D>();
       originalColliderSize = capsuleCollider.size;
        isOnGround = true;
        startingPosition = transform.position;
        

    }

    // Update is called once per frame
    void Update()
    {
       
        //Player AutoRun to right 
        AutoRun();

        //Crouch when Left ctrl is pressed for 2 seconds
        
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            Crouch();
            Invoke("StandUp", crouchTime); //Automaticallt stand up after 2 seconds
            
        }

        //Jump when Space is pressed
        if (Input.GetKeyDown(KeyCode.Space) && isOnGround)
        {
            Jump();
            canDoubleJump = true;
            isOnGround=false;
        }

        // Double jump when V is pressed
        if ((Input.GetKeyDown(KeyCode.V)) && !isOnGround && canDoubleJump)
        {
            Jump();
            canDoubleJump = false;
        }

        

        if (transform.position.y < -5) //Respawner.
        {
            //transform.position = new Vector3 (0.0f,0.0f, 0.0f); //This just respawns it over a pit its fucking hilarious if you unlock the rotation.
            //transform.position = startingPosition;
            Respawn();
        }
        
        

    }
    void AutoRun()
    {
        float dt = Time.deltaTime;
        Vector2 direction = Vector2.right;
        Vector3 change = direction*speed * dt;
        transform.position += change;

    }
    public void Crouch() 
    { 
        isCrouching = true;
    
        capsuleCollider.size = new Vector2(capsuleCollider.size.x, originalColliderSize.y / 2);

    }
    void StandUp()
    {
        capsuleCollider.size = originalColliderSize;
    }
    void Jump()
    {
        rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
            if (other.transform.CompareTag("Ground"))
            {

                isOnGround = true;
            }
        if (other.transform.CompareTag("Window"))
        {
            Respawn();
        }
        
          
    }
    public void Respawn()
    {
        transform.position = startingPosition;

    }

}
