using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Health : MonoBehaviour
{
    public int health;
    public int MaxHealth;
    public int Lives;
    public Slider healthSlider;
    public GameObject gameOverPanel;
    public AudioManager audioManager;

    bool Vulnerable = true;
    // Start is called before the first frame update
    void Start()
    {
       FindAnyObjectByType<LifelineManager>();
        // Ensure Game Over Panel is initially inactive
        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(false);
        }


        // Ensure you have an AudioManager in the scene and it's assigned
        if (audioManager == null)
        {
            audioManager = FindObjectOfType<AudioManager>();
        }

        // the health slider
        if (healthSlider != null)
        {
            healthSlider.maxValue = MaxHealth;
            healthSlider.value = health;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (health <= 0)
        {
            RespawnAtCheckpoint();
            FindAnyObjectByType<LifelineManager>().LoseLife();
            audioManager.PlaySFX(audioManager.lostlife);
            //Destroy(gameObject); //eventually this will be a respawn function

        }

        if (health > MaxHealth)
        {
            health = MaxHealth;
        }
        
        if (Vulnerable == false)
        {
            //there should eventually time this back to true so the player can be hit again. 
        }
        if (Lives <= 0)
        {
            // Activate the Game Over Panel
             if (gameOverPanel != null)
             {
                gameOverPanel.SetActive(true);
                audioManager.PlaySFX(audioManager.gameOver);
            }
        }

        if (healthSlider != null)
        {
            healthSlider.value = health;
        }
    }

    public void HealthBoost()
    {
        health = health + 2;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
       if (Vulnerable == true && collision.gameObject.tag == "Damager" || collision.gameObject.tag == "Hostile") //eventually tags will need to be added to this so the player is only damaged when hitting enemies or obstacles 
        {
            health = health - 10;
            Vulnerable = false;
            audioManager.PlaySFX(audioManager.damage);
        }
       if (collision.gameObject.tag == "InstantDeath")
        {
            health = 0;
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Damager" || collision.gameObject.tag == "Hostile")
        {
            Vulnerable = true;
        }

    }

    private void RespawnAtCheckpoint()
    {
        health = MaxHealth;
        Lives = Lives - 1; 
        // Move player to the last checkpoint
        transform.position = CheckpointManager.Instance.GetLastCheckpointPosition();
        Debug.Log("Player respawned at checkpoint!");
    }
}
