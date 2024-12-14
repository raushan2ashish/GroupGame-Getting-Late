using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DogEnemyScript : MonoBehaviour
{
    bool Activated = false;
    bool Damage = false;
    public float DogEnemySpeed = 5.0f;
    public float timeLeft = 6.0f;
    public Vector2 initPos;
    public Rigidbody2D RigBod;
    public Pctrl pctrl;
    
    // Stores the initial Enemy positions
    void Start()
    {
        initPos = transform.position;
        RigBod = GetComponent<Rigidbody2D>();
    }

    // Moves enemy after activation
    void Update()
    {
        if(Activated == true)
        {
            //RigBod.velocity = Vector2.left * DogEnemySpeed;
            //Timer for respawning enemies, and respawner
            timeLeft -= Time.deltaTime;
            if(timeLeft <= 0)
            {
                Activated = false;
                Damage = false;
                transform.position = initPos;
            }
        }


    }

    //Checks if the player entered trigger
    public void MovementStart()
    {
        Activated = true;
        RigBod.velocity = Vector2.left * DogEnemySpeed;
        Damage = false;
    }

    public void MovementEnd()
    {
        Damage = true;   
    }

    private void OnCollisionEnter2D(Collision2D other) 
    {
        if(Damage == true && other.gameObject.tag == "Player")
        {
            pctrl.Respawn();
            Damage = false;
            Activated = false;
            transform.position = initPos;
            //Resetter();
        }
    }

    public void Resetter()
    {
        Activated = false;
        Damage = false;
        transform.position = initPos;
    }
}
