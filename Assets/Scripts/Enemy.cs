using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    
    public int health;
    public bool isActive;
    public bool facingRight = true;
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
        }
        else if(rb.velocity.x > 0)
        {
            facingRight = true;
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
        Vector3 scaler = transform.localScale;
        scaler.x *= -1;
        transform.localScale = scaler;
    }

}
