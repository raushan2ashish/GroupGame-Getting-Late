using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    
    public int health;
    public bool isActive;
    public bool isIdle = true;
    public bool movingRight = false;
    public bool facingRight = false;
    public Rigidbody2D rb;
    

    // Start is called before the first frame update
    public void Start()
    {
        GetComponent<Rigidbody2D>();
        health = 2;
        isActive = false;
    }

    // Update is called once per frame
    public void Update()
    {
        if(rb.velocity.x < 0)
        {
            facingRight = false;
            isIdle = false;
            AnimationFlipper();
        }
        else if(rb.velocity.x > 0)
        {
            facingRight = true;
            isIdle = false;
            AnimationFlipper();
        }
        else if(rb.velocity.x == 0)
        {
            isIdle = true;
        }

        
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Weapon")
        {
            health -= 1;
            Debug.Log("Health: " + health);
            if(health <= 0)
            {
                Destroy(gameObject);
            }
        }
    }

    public void AnimationFlipper()
    {
        if(movingRight != facingRight && isIdle == false)
        {
            movingRight = !movingRight;
            Vector3 scaler = transform.localScale;
            scaler.x *= -1;
            transform.localScale = scaler;
        }
    }

}
