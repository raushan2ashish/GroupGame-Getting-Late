using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ItemScript : MonoBehaviour
{
    public bool Coin;
    public bool TimeReduction;
    public bool HealthUp;
    // Start is called before the first frame update
    void Start()
    {
        FindAnyObjectByType<Health>();
        FindAnyObjectByType<Timer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Object.Destroy(gameObject);

        if (Coin)
        {
            FindAnyObjectByType<Scoreboard>().CoinPoint();
        }
        if (TimeReduction)
        {
            FindAnyObjectByType<Timer>().TimePowerUp();
            FindAnyObjectByType<Scoreboard>().TimePoint();  
        }
        if(HealthUp)
        {
            FindAnyObjectByType<Health>().HealthBoost();
            FindAnyObjectByType<Scoreboard>().HealthPoint();
        }
    }
}
