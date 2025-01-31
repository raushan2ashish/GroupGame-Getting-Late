using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack1 : MonoBehaviour
{
    public string attackButton = "Attack"; // Button assigned for attack (F)
    public Transform attackPoint; // Empty GameObject for attack position
    public float attackRange = 0.5f; // Attack hitbox size
    public LayerMask enemyLayers; // Layer to detect enemies
    public float attackRate = 0.5f; // Delay between attacks
    private float nextAttackTime = 0f;


    private Animator anim; // Reference to Animator

    void Start()
    {
        anim = GetComponent<Animator>(); // Get the Animator component
    }

    // Update is called once per frame
    void Update()
    {
        // Attack when holding the "F" button with cooldown
        if (Input.GetButton(attackButton) && Time.time >= nextAttackTime)
        {
            Attack();
            nextAttackTime = Time.time + attackRate; // Set next attack time
        }
    }

    private void Attack()
    {
        // Play attack animation
        anim.SetTrigger("Attack");

        // Detect enemies in range
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

        // Damage enemies
        foreach (Collider2D enemy in hitEnemies)
        {
            Debug.Log("Hit: " + enemy.name);
            // Here, you can call an enemy damage function like:
            // enemy.GetComponent<Enemy>().TakeDamage(damageAmount);
        }

    }
    // Show attack range in Scene view (for debugging)
    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null) return;
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }


}
