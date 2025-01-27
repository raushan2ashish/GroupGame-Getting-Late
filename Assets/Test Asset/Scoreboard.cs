using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Scoreboard : MonoBehaviour
{
    int Score;

    public int CoinValue;
    public int TimeValue;
    public int HealthValue;

    public Text text;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        text.text = "Score: " + Score;
    }

    public void CoinPoint()
    {
        Score = Score + CoinValue;
    }
    public void TimePoint()
    {
        Score = Score + TimeValue;
    }
    public void HealthPoint()
    {
        Score = Score + HealthValue;   
    }
}
