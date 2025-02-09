using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelEndCheckpoint : MonoBehaviour
{
    public GameObject checkpointPanel; // Reference to the checkpoint UI panel
    public Text timeText; // Reference to the UI text to display the time
    public Button nextLevelButton; // Reference to the button to move to the next level

    private void Start()
    {
        // Ensure the panel is hidden at the start
        checkpointPanel.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Display the time taken
            float timeTaken = Time.timeSinceLevelLoad; // Get the time taken since the level started
            timeText.text = "Time Taken: " + timeTaken.ToString("F2") + " seconds";

            // Show the checkpoint panel
            checkpointPanel.SetActive(true);

            // Freeze the game by setting time scale to 0
            Time.timeScale = 0;

            // Add a listener to the next level button
            nextLevelButton.onClick.AddListener(NextLevel);
        }
    }

    private void NextLevel()
    {
        // Resume the game by setting time scale back to 1
        Time.timeScale = 1;

        // Load the next level (change "Level2" to your scene name)
        SceneManager.LoadScene("Level 2");
    }
}
