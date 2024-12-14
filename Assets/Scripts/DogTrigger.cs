using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Animations;

public class DogTrigger : MonoBehaviour
{
    //Separate trigger for the dog to activate
    public DogEnemyScript Triggered;
    private void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.gameObject.tag == "Player")
        {
            Triggered.MovementStart();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if(other.gameObject.tag == "Player")
        {
            Triggered.MovementEnd();
        }
    }
}
