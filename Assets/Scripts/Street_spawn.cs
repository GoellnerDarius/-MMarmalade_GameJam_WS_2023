using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Street_spawn : MonoBehaviour
{

    public float Counter = 0;
    public GameObject Street;
    public Text ViewCount;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Counter += 1 * Time.deltaTime;
        ViewCount.text = "Time:" + Math.Round(Counter,1);
        if(Counter >= 5 && Counter <= 6)
        {
            Console.WriteLine(" Der gamejam suckt");
            Counter = 0;
        }
    }
}
