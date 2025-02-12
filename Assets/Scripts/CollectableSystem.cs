using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CollectableSystem : MonoBehaviour
{
    public int Collectable;
    public int CollectableLimit;
    public Text text;

    // Start is called before the first frame update
    void Start()
    {
  
    }

    // Update is called once per frame
    void Update()
    {
        text.text = "Collectables:" + Collectable + "/" + CollectableLimit;
    }
    public void CollectableObtained()
    {
        Collectable = Collectable + 1;
    }
}
