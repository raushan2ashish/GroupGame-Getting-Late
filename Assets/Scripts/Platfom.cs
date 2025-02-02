using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platfom : MonoBehaviour
{
    public bool ice;
    public bool mud;

    // Start is called before the first frame update
    void Start()
    {
        FindAnyObjectByType<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
      if (mud)
        {
            FindAnyObjectByType<PlayerMovement>().mud();
        }
      else if (ice)
        {
            FindAnyObjectByType<PlayerMovement>().Ice();
        }
      else
        {
            FindAnyObjectByType<PlayerMovement>().normal();
        }
    }
}
