using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UmbrellaAttack : MonoBehaviour
{
    
    [SerializeField] public Enemy enemy;
    [SerializeField] public Collider2D damageArea;
    [SerializeField] public float attackTimer = 2.0f;
    [SerializeField] public float hitTimer = 0.2f;
    public float attackTimerReset;
    public float hitTimerReset;
    
    
    // Start is called before the first frame update
    public void Start()
    {
        damageArea.GetComponent<Collider2D>();
        attackTimerReset = attackTimer;
        damageArea.enabled = false;
    }

    // Update is called once per frame
    public void Update()
    {
        attackTimer -= Time.deltaTime;
        if((Input.GetKeyDown(KeyCode.LeftShift)) && attackTimer <= 0)
        {
            damageArea.enabled = !damageArea.enabled;
            //AttackAreaResetter();
            attackTimerReset = attackTimer;
        }

        //hitTimer -= Time.deltaTime;
        //if(hitTimer <= 0)
        //{
        //
        //}
    }

    public void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.gameObject.tag == "Enemy")
        {
            enemy.HpCalculator();
        }
    }

    public void AttackAreaResetter()
    {
        //hitTimer -= Time.deltaTime;
        //if(hitTimer)
        //damageArea.enabled = !damageArea.enabled;
    }

}
