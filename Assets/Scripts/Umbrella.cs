using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Umbrella : MonoBehaviour
{
    public Collider2D colArea;
    public PlayerMovement player;
    
    // Start is called before the first frame update
    public void Start()
    {
        colArea.GetComponent<Collider2D>();
        colArea.enabled = false;
    }

    public void ShieldSwitch()
    {
        colArea.enabled = !colArea.enabled;
    }
}

