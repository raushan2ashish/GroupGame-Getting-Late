using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class WindowControl : MonoBehaviour
{
    private Animator _animator;
    
    
    // Start is called before the first frame update
    void Start()
    {
        _animator = GetComponent<Animator>();
       

        

        
    }
    private void OnTriggerEnter2D(Collider2D other)

    {
        // Check if the player is the one entering the trigger
        if (other.CompareTag("Player"))
        {
            // If the player is within range, open the window
            OpenWindow();

        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        // Close the window when the player leaves the area
        if (other.CompareTag("Player"))
        {
            CloseWindow();
        }
    }
    void OpenWindow()
    {
        if (_animator != null)
        {
            _animator.SetBool("IsOpen", true);  // Assumes an animation parameter "IsOpen"
        }

    }
    void CloseWindow()
    {
        if (_animator != null)
        {
            _animator.SetBool("IsOpen", false);
        }
    }
}
