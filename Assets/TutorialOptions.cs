using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialOptions : MonoBehaviour
{
    
    [SerializeField] public GameObject switchObj;
    [SerializeField] public float holdTime = 1.0f;
    public bool toggleTutorial = true;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    public void Update()
    {
        
        if(Input.GetKeyDown(KeyCode.T))
        {
            toggleTutorial = !toggleTutorial;
            switchObj.SetActive(toggleTutorial);
        }
    }
}
