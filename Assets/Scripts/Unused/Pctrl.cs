using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Pctrl : MonoBehaviour
{
    public Animator anim;
    public DogEnemyScript dogEnemyScrippt;
    public float jumpForce = 3.0f;
    public float crouchTime = 2.0f;
    public int testLife = 5;
    public Vector3 startingPosition;
    private bool canDoubleJump = true;
    private bool isCrouching = false;
    public bool TestMap;
    public bool isShielding = false;

    public Rigidbody2D rb;
    private CapsuleCollider2D capsuleCollider;
    private Vector2 originalColliderSize;
    private Vector2 originalColliderOffset;
    private bool isOnGround;

    AudioManager audioManager;

    //public Animator runAnimation;

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        capsuleCollider = GetComponent<CapsuleCollider2D>();
        originalColliderSize = capsuleCollider.size;
        originalColliderOffset = capsuleCollider.offset;
        isOnGround = true;
        startingPosition = transform.position;


    }

    // Update is called once per frame
    void Update()
    {

        if (isOnGround)
        {
            anim.SetBool("isJumping", false);
        
        }
        if (!isOnGround)
        {
            anim.SetBool("isJumping", true);
        
        }

        //Crouch when Left ctrl is pressed for 2 seconds

        if (isOnGround && Input.GetKeyDown(KeyCode.LeftControl) || Input.GetKeyDown(KeyCode.RightControl))
        {
            Crouch();
            
            Invoke("StandUp", crouchTime); //Automaticallt stand up after 2 seconds

            //runAnimation.SetInteger("moveControl", 1);
            
            if (isCrouching )
            {
                anim.SetBool("isSliding", true);
            }
            else if (!isCrouching)
            {
                anim.SetBool("isSliding", false);
            }

            
        }

       

        //Jump when Space is pressed
        if (isOnGround && Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
            canDoubleJump = true;
            isOnGround=false;

            
            //runAnimation.SetInteger("moveControl", 2);
        }

        // Double jump when V is pressed
        else if ((Input.GetKeyDown(KeyCode.Space)) && !isOnGround && canDoubleJump)
        {
            DoubleJump();
            canDoubleJump = false;
            //runAnimation.SetInteger("moveControl", 2);
        }

        

        if (transform.position.y < -5) //Respawner.
        {
            //transform.position = new Vector3 (0.0f,0.0f, 0.0f); //This just respawns it over a pit its fucking hilarious if you unlock the rotation.
            //transform.position = startingPosition;
            Respawn();
        }

        
        
        

    }
    void PickAnimState()
    {



    }
    public void Crouch() 
    {
        //audioManager.PlaySFX(audioManager.slide);
        isCrouching = true;
        
        anim.SetBool("isSliding", true);
        

        capsuleCollider.size = new Vector2(capsuleCollider.size.x, originalColliderSize.y / 2);
        capsuleCollider.offset = new Vector2(originalColliderOffset.x, originalColliderOffset.y + (originalColliderSize.y / 4));
        
   
            
        
    }
    void StandUp()
    {
        capsuleCollider.size = originalColliderSize;
        capsuleCollider.offset = originalColliderOffset;
        anim.SetBool("isSliding", false);
        anim.SetBool("isSliding", false);



    }
    void Jump()
    {
        rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        audioManager.PlaySFX(audioManager.jump);
        
    }
    void DoubleJump()
    {
        rb.AddForce(Vector2.up * jumpForce/2, ForceMode2D.Impulse);
        audioManager.PlaySFX(audioManager.jump);
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.transform.CompareTag("Ground"))
        {
            isOnGround = true;
        }
        else if (other.transform.CompareTag("Window"))
        {
            audioManager.PlaySFX(audioManager.objectcollide);
            Respawn();
        }
        else if (other.gameObject.tag == "Hostile" || other.gameObject.tag == "Enenmy")
        {
            TestLifeCounter();
        }
        
          
    }
    // Function accessed by the enemy or hostile object
    // Counts the number of test lifes and prints to console
    public void TestLifeCounter()
    {
        testLife -= 1;
        Debug.Log("Life: " + testLife);
    }
    
    public void Respawn()
    {
        if (TestMap)
        {
            transform.position = startingPosition;
        }
        else
        {
            SceneManager.LoadSceneAsync("Level1");
        }
    }

}
