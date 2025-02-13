using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class EnemyTerritoryScript : MonoBehaviour
{
    
    [SerializeField] public GameObject enemyObj;
    [SerializeField] public float enemySpeed = 2.0f;
    //public float enemyVerticalPos;
    //public float enemyVerticalPosLock;
    public Vector2 enemyReturnCheck = new Vector2(1, 1);
    public bool enemyActive = false;
    

    public void Start() 
    {
        //enemyVerticalPos = enemyObj.GetComponent<Rigidbody2D>().velocity.y;
        //enemyVerticalPosLock = enemyVerticalPos;
        //Debug.Log("is: " + enemyVerticalPos);
    }

    // Makes enemy stop when it gets back to the starting point
    // Locks enemy movement on y-axis
    void Update()
    {
        if (enemyObj != null)
        {
            if ((transform.position - enemyObj.transform.position).sqrMagnitude <= enemyReturnCheck.sqrMagnitude && enemyActive == false)
            {
                enemyObj.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
                enemyObj.transform.position = transform.position;
            }
        }
    }

    //Activates the enemy when player enters
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (enemyObj != null)
        {
            if (other.gameObject.tag == "Player")
            {
                enemyActive = true;
                enemyObj.GetComponent<Rigidbody2D>().velocity = (other.transform.position - enemyObj.transform.position).normalized * enemySpeed;
            }
        }
    }

    //Updates player position and enemy direction
    public void OnTriggerStay2D(Collider2D other)
    {
        if (enemyObj != null)
        {
            if (other.gameObject.tag == "Player")
            {
                enemyObj.GetComponent<Rigidbody2D>().velocity = (other.transform.position - enemyObj.transform.position).normalized * enemySpeed;
            }
        }
    }

    //Returns enemy to starting point if player or enemy leaves territory
    public void OnTriggerExit2D(Collider2D other)
    {
        if (enemyObj != null)
        {
            if (other.gameObject.tag == "Player")
            {
                enemyActive = false;
                enemyObj.GetComponent<Rigidbody2D>().velocity = (transform.position - enemyObj.transform.position).normalized * enemySpeed;
            }
            else if (other.gameObject.tag == "Hostile")
            {
                enemyObj.GetComponent<Rigidbody2D>().velocity = (transform.position - enemyObj.transform.position).normalized * enemySpeed;
                //enemyObj.transform.position = transform.position;
            }
        }
    }
}
