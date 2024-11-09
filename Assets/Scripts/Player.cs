using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Player : MonoBehaviour
{
    Vector2 Direction = Vector2.right; //autorunner
    CapsuleCollider2D PlayerBox;
    Vector2 Jump = Vector2.up;
    
    // Start is called before the first frame update
    void Start()
    {
        PlayerBox = GetComponent<CapsuleCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        float speed = 5.0f; 
        float dt = Time.deltaTime;
        Vector3 change = Direction * speed * dt;
        transform.position += change;
        //all stuff that dictates how the character moves.

        float Height = PlayerBox.size.y; //for the croutch 

       if (Input.GetKeyDown(KeyCode.RightControl) || Input.GetKeyDown(KeyCode.LeftControl)) //Crouch. DOESNT WORK!!!!!!
       {
            Height = Height/2;
            Debug.Log(PlayerBox.size.y);
       }
       if(transform.position.y < -5 ) //Respawner.
       {
           //transform.position = new Vector3 (0.0f,0.0f, 0.0f); //This just respawns it over a pit its fucking hilarious if you unlock the rotation.
           transform.position = new Vector3 (-11.5f, -1.16f); //(X,Y);
       }
    }
}
