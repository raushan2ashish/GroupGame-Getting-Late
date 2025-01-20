using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public float timer = 0.0f;
    public Text text;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frameW 
    void Update()
    {
        timer += Time.deltaTime;
        text.text = "Time: " + timer;
    }

}
