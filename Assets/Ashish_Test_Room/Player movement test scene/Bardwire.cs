using UnityEngine;

public class Bardwire : MonoBehaviour
{
    public int health = 100;
    public int damageToPlayer = 10; // Damage dealt to the player on contact

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if the colliding object is the player
        if (collision.gameObject.CompareTag("Player"))
        {
            // Get the PlayerMovementControl component and apply damage
            PlayerMovementControl player = collision.gameObject.GetComponent<PlayerMovementControl>();
            if (player != null)
            {
                player.TakeDamage(damageToPlayer);
                Debug.Log("Player took damage from enemy!");
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Optional: Use this if you want to use triggers instead of collisions
        if (collision.CompareTag("Player"))
        {
            PlayerMovementControl player = collision.GetComponent<PlayerMovementControl>();
            if (player != null)
            {
                player.TakeDamage(damageToPlayer);
                Debug.Log("Player took damage from enemy!");
            }
        }
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        // Add death logic here, such as playing a death animation or destroying the GameObject
        Destroy(gameObject);
    }
}
