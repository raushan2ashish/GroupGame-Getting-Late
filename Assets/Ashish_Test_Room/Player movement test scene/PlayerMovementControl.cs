using System.Collections;
using UnityEngine;

public class PlayerMovementControl : MonoBehaviour
{

    // Movement Parameters
    public float walkSpeed = 5f;
    public float runSpeed = 8f;
    public float jumpForce = 23f;
    public float glideFallSpeed = 2f; // Adjustable glide fall speed
    public float fallSpeed = 50f; // Adjustable normal fall speed
    public float gravityScale = 6f; // Adjustable gravity scale
    public float shieldSpeedMultiplier = 0.5f;
    public LayerMask groundLayer;
    public Transform groundCheck;
    public float groundCheckRadius = 0.2f;

    // Attack Parameters
    public Transform attackPoint;
    public float attackRange = 0.5f;
    public LayerMask enemyLayer;
    public int attackDamage = 20;
    private bool isAttacking = false; // New boolean for attack animation

    // Ladder Climbing
    private bool isClimbing = false;
    private Rigidbody2D rb;
    private Animator animator;
    private bool facingRight = true;

    // Health and Lifeline UI
    //public HealthBar healthBar;
    //public LifelineIndicator lifelineIndicator;

    private bool isGrounded;
    private float originalGravityScale;
    private bool isGliding = false;
    private bool isShielding = false;

    // Gizmo Controls
    public bool drawGizmos = true; // Enable/disable drawing Gizmos

    public AudioManager audioManager;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        originalGravityScale = rb.gravityScale;
        rb.gravityScale = gravityScale;

        // Ensure you have an AudioManager in the scene and it's assigned
        if (audioManager == null)
        {
            audioManager = FindObjectOfType<AudioManager>();
        }
    }

    private void Update()
    {
        HandleMovement();
        HandleJump();
        HandleGlide();
        HandleShield();
        HandleAttack();
        HandleClimbLadder();

        // Ground Check
        isGrounded = IsGrounded();

        // Flip character based on movement direction
        if (rb.velocity.x > 0 && !facingRight)
        {
            Flip();
        }
        else if (rb.velocity.x < 0 && facingRight)
        {
            Flip();
        }

        // Apply fall speed control
        if (!isGrounded && !isGliding)
        {
            rb.velocity = new Vector2(rb.velocity.x, Mathf.Max(rb.velocity.y, -fallSpeed));
        }

        // Update animator
        animator.SetBool("IsJumping", !isGrounded);
        animator.SetBool("IsShielding", isShielding);
        animator.SetBool("IsAttacking", isAttacking);
    }

    private void HandleMovement()
    {
        float moveInput = Input.GetAxisRaw("Horizontal");
        float speed = walkSpeed;

        if (isShielding)
        {
            speed *= shieldSpeedMultiplier;
        }
        else if (Input.GetKey(KeyCode.LeftShift) && moveInput != 0)
        {
            speed = runSpeed;
            animator.SetBool("IsRunning", true);
        }
        else
        {
            animator.SetBool("IsRunning", false);
        }

        if (moveInput != 0)
        {
            animator.SetBool("IsWalking", true);
        }
        else
        {
            animator.SetBool("IsWalking", false);
        }

        Vector2 velocity = rb.velocity;
        velocity.x = moveInput * speed;
        rb.velocity = velocity;
    }

    private void HandleJump()
    {
        if (!isShielding && Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            audioManager.PlaySFX(audioManager.jump);
        }
    }

    private void HandleGlide()
    {
        if (!isShielding && Input.GetKey(KeyCode.G) && !isGrounded && rb.velocity.y < 0)
        {
            isGliding = true;
            rb.velocity = new Vector2(rb.velocity.x, -glideFallSpeed);
            animator.SetBool("IsGliding", true);
            
        }
        else
        {
            if (isGliding)
            {
                isGliding = false;
                animator.SetBool("IsGliding", false);
                
            }
            //isGliding = false;
           // animator.SetBool("IsGliding", false);
            
        }
    }

    private void HandleShield()
    {
        isShielding = Input.GetKey(KeyCode.H);
    }

    private void HandleAttack()
    {
        if (Input.GetKeyDown(KeyCode.F) && !isAttacking)
        {
            isAttacking = true;
            animator.SetBool("IsAttacking", true);
            audioManager.PlaySFX(audioManager.attack);

            // Detect enemies in range of the attack
            Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayer);

            // Damage them
            foreach (Collider2D enemy in hitEnemies)
            {
                enemy.GetComponent<EnemyTest>().TakeDamage(attackDamage);
            }

            // Reset attack state after a short delay
            StartCoroutine(ResetAttack());
        }
    }

    private IEnumerator ResetAttack()
    {
        yield return new WaitForSeconds(0.5f); // Adjust the delay to match the attack animation duration
        isAttacking = false;
        animator.SetBool("IsAttacking", false);
    }

    private void HandleClimbLadder()
    {
        if (isClimbing && !isShielding)
        {
            float verticalInput = Input.GetAxisRaw("Vertical");
            rb.velocity = new Vector2(rb.velocity.x, verticalInput * walkSpeed);
            rb.gravityScale = 0;

            // Ensure the player always faces right while climbing
            if (!facingRight)
            {
                Flip();
            }

            if (verticalInput != 0)
            {
                animator.SetBool("IsClimbing", true);
                animator.SetBool("IsLadderGrab", false);
            }
            else
            {
                animator.SetBool("IsClimbing", false);
                animator.SetBool("IsLadderGrab", true);
            }
        }
        else
        {
            rb.gravityScale = gravityScale;
            animator.SetBool("IsClimbing", false);
            animator.SetBool("IsLadderGrab", false);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Ladder"))
        {
            isClimbing = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Ladder"))
        {
            isClimbing = false;
        }
    }

    private bool IsGrounded()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(groundCheck.position, groundCheckRadius, groundLayer);
        foreach (Collider2D collider in colliders)
        {
            // Check if the collision is with the ground and not with vertical surfaces
            if (Vector2.Dot(collider.transform.up, Vector2.up) > 0.5f)
            {
                return true;
            }
        }
        return false;
    }

    private void OnDrawGizmos()
    {
        if (!drawGizmos) return;

        // Gizmo visualizations for walkJump, runJump, and glide paths
        if (facingRight)
        {
            Gizmos.color = Color.green; // WalkJump path
            DrawJumpPath(walkSpeed, true);

            Gizmos.color = Color.blue; // RunJump path
            DrawJumpPath(runSpeed, true);

            Gizmos.color = Color.yellow; // Glide path
            DrawGlidePath(true);
        }
        else
        {
            Gizmos.color = Color.green; // WalkJump path
            DrawJumpPath(walkSpeed, false);

            Gizmos.color = Color.blue; // RunJump path
            DrawJumpPath(runSpeed, false);

            Gizmos.color = Color.yellow; // Glide path
            DrawGlidePath(false);
        }

        // Draw attack range Gizmo
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }

    private void DrawJumpPath(float speed, bool facingRight)
    {
        Vector2 startPosition = transform.position;
        Vector2 velocity = new Vector2(facingRight ? speed : -speed, jumpForce);
        for (float t = 0; t < 2; t += 0.1f)
        {
            // Account for adjustable gravity scale in the jump path
            Vector2 point = startPosition + (velocity * t) + 0.5f * Physics2D.gravity * gravityScale * t * t;
            Gizmos.DrawSphere(point, 0.1f);
        }
    }

    private void DrawGlidePath(bool facingRight)
    {
        Vector2 startPosition = transform.position;
        for (float t = 0; t < 2; t += 0.1f)
        {
            // Account for adjustable gravity scale in the glide path
            Vector2 point = startPosition + new Vector2(facingRight ? 0 : 0, -glideFallSpeed * t);
            Gizmos.DrawSphere(point, 0.1f);
        }
    }

    private void Flip()
    {
        facingRight = !facingRight;
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }
}
