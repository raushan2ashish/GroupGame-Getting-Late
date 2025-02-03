using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    public GameObject gameOverPanel; // Reference to the game over UI panel

    private void Start()
    {
        // Ensure the panel is hidden at the start
        gameOverPanel.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Show the game over panel
            gameOverPanel.SetActive(true);

            // Freeze the game by setting time scale to 0
            Time.timeScale = 0;
        }
    }

    public void RestartLevel()
    {
        // Resume the game by setting time scale back to 1
        Time.timeScale = 1;

        // Reload the current scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
