using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerBrick : MonoBehaviour
{
    [SerializeField] public float waitTime = 5.0f;
    [SerializeField] public float timerTime = 5.0f;
    [SerializeField] public GameObject brickPrefab;
    public bool isSpawning;
     
    
    
    public void Start() 
    {
        waitTime = timerTime;
    }
    
    public void Update()
    {
        if(isSpawning == false)
        {
            timerTime -= Time.deltaTime;
            if(timerTime <= 0)
            {
                isSpawning = true;
                timerTime = waitTime;
                ObjectSpawner();
            }
        }
    }

    public void ObjectSpawner()
    {
        Instantiate(brickPrefab, transform.position, Quaternion.identity);
        isSpawning = false;
    }
}
