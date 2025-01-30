using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Umbrella : MonoBehaviour
{
    public bool isShielding = true;
    public Collider2D colArea;
    
    // Start is called before the first frame update
    void Start()
    {
        colArea.GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.C))
        {
            isShielding = !isShielding;
            colArea.enabled = !colArea.enabled;
            Debug.Log("Is shielding: " + isShielding);
        }
    }

    public void OnCollisionEnter2D(Collision2D other) 
    {
        if(isShielding == true)
        {
            
        }
    }
}
