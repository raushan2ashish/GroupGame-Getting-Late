using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    
    public int health;
    

    // Start is called before the first frame update
    public void Start()
    {
        health = 2;
    }

    // Update is called once per frame
    public void Update()
    {
        
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
    
    public void HpCalculator()
    {
        //health -= 1;
        //Debug.Log("Health: " + health);
        //if(health <= 0 && )
        //{
        //    Destroy(gameObject);
        //}
    }

    //public void SelfDestruction()
    //{
    //    Destroy(gameObject);
    //}
}
