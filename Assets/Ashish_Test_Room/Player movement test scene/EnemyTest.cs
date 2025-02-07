using UnityEngine;

public class EnemyTest : MonoBehaviour
{
    public int health = 100;

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
