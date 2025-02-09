using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointTrigger : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // Set the checkpoint to this position
            CheckpointManager.Instance.SetCheckpoint(transform.position);
            Debug.Log("Player reached checkpoint!");
        }
    }
}
