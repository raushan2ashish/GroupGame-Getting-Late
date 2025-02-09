using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointManager : MonoBehaviour
{
    public static CheckpointManager Instance; // Singleton for easy access
    private Vector2 lastCheckpointPosition;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Persist across scene loads if needed
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SetCheckpoint(Vector2 position)
    {
        lastCheckpointPosition = position;
        Debug.Log("Checkpoint set at: " + lastCheckpointPosition);
    }

    public Vector2 GetLastCheckpointPosition()
    {
        return lastCheckpointPosition;
    }
}
