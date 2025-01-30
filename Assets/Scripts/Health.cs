using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Health : MonoBehaviour
{
    public int health;
    public int MaxHealth;

    bool Vulnerable = true;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (health <= 0)
        {
            Debug.Log("player dead");
            Destroy(gameObject); //eventually this will be a respawn function

        }

        if (health > MaxHealth)
        {
            health = MaxHealth;
        }
        
        if (Vulnerable == false)
        {
            //there should eventually time this back to true so the player can be hit again. 
        }
    }

    public void HealthBoost()
    {
        health = health + 2;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
       if (Vulnerable == true && collision.gameObject.tag == "Damager") //eventually tags will need to be added to this so the player is only damaged when hitting enemies or obstacles 
        {
            health = health - 1;
            Vulnerable = false;
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Damager")
        {
            Vulnerable = true;
        }

    }
}
