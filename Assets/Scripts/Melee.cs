using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Melee : MonoBehaviour
{
    
    public Collider2D colArea;
    //public bool attackReady;
    public bool attackStarted;
    [SerializeField] public float attackCooldown = 0.2f;
    [SerializeField] public float timerResetVal = 0.2f;

    // Start is called before the first frame update
    public void Start()
    {
        colArea.GetComponent<Collider2D>();
        colArea.enabled = false;
        //attackReady = false;
        attackStarted = false;
    }

    // Update is called once per frame
    public void Update()
    {
        if(attackStarted == true)
        {
            attackCooldown -= Time.deltaTime;
            if(attackCooldown <= 0)
            {
                colArea.enabled = !colArea.enabled;
                attackCooldown = timerResetVal;
                attackStarted = !attackStarted;
            }
        }

        if(Input.GetKeyDown(KeyCode.F) && attackStarted == false)
        {
            attackStarted = !attackStarted;
            colArea.enabled = !colArea.enabled;
        }
    }

}
