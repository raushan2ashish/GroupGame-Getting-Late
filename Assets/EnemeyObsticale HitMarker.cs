using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemeyObsticaleHitMarker : MonoBehaviour
{
    public int health;
    bool vulnerable;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }
    public void TakeDamage(int damage)
    {
        health -= 1;
    }

}
