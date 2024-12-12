using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Checkpoint1 : MonoBehaviour
{
    private void Start()
    {
        checkpoint1.SetActive(false);
    }
    public GameObject checkpoint1;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            checkpoint1.SetActive(true);
            Time.timeScale = 0f;
        }
    }
    
}
