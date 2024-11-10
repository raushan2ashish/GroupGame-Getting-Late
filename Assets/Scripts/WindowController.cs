using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindowController : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;

    public Sprite greenWindow;  // Green sprite (closed window)
    public Sprite redWindow;    // Red sprite (opened window)


    // Start is called before the first frame update
    void Start()
    {
        
        // Get the SpriteRenderer component
        spriteRenderer = GetComponent<SpriteRenderer>();

        // Initially set the window to be closed (green)
        spriteRenderer.sprite = greenWindow;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Debug.Log("Player entered the window trigger.");
            //
            // When the player enters the trigger area, set the window to open (red)
            OpenWindow();
           
        
        }
        

    }
    


    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // When the player leaves the trigger area, set the window to closed (green)
            CloseWindow();
        }
    }
    void OpenWindow()
    {
        // Change the sprite to red (opened window)
        spriteRenderer.sprite = redWindow;
    }
    void CloseWindow()
    {
        // Change the sprite to green (closed window)
        spriteRenderer.sprite = greenWindow;
    }
    

}
