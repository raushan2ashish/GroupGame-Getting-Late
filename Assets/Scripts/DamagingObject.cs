using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DamagingObject : MonoBehaviour
{
    public Pctrl pctrl;
    [SerializeField] public GameObject player;
    public Vector2 pos1;
    public Vector2 pos2;
    public float waitTimePos = 0.1f;
    public int posRepeat = 9;
   
    //Start is used as a coroutine to delay position check 1 and 2
    //If object reaches ground or gets stuck it is destroyed
    public IEnumerator Start() 
    {
        pos2 = new Vector2(-1, -1);
        for(int i = 0; i < posRepeat; i++)
        {
            pos1 = transform.position;
            if(pos1 == pos2)
            {
                Destroy(gameObject);
            }
            pos2 = transform.position;
            yield return new WaitForSeconds(1);
        }

        pos1 = transform.position;
        pos2 = new Vector2(-1, 1);
    }
    
    //Checks collision with player to reduce test lives and self-destruct
    public void OnCollisionEnter2D(Collision2D other) 
    {
        if(other.gameObject.tag == "Player")
        {
            Destroy(gameObject);
        }
    }

    public void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.gameObject.tag == "Umbrella" || other.gameObject.tag == "Weapon") 
        {
            Destroy(gameObject);
        }
    }

    

}
