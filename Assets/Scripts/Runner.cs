using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Runner : MonoBehaviour
{
    Vector2 Direction = Vector2.right; //autorunner
    CapsuleCollider2D PlayerBox;
    
    // Start is called before the first frame update
    void Start()
    {
        PlayerBox = GetComponent<CapsuleCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        float Runspeed = 10.0f;
        float dt = Time.deltaTime;
        Vector3 change = Direction * Runspeed * dt;
        transform.position += change;
        //all stuff that dictates how the character moves.
    }

}
