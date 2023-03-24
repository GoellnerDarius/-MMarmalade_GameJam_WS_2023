using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Street_spawn : MonoBehaviour
{

    public float Counter = 0;
    public GameObject Street;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Counter += 1 * Time.deltaTime;
        if(Counter >= 5 && Counter <= 6)
        {
            Instantiate(Street,new Vector3(0f,-40f,-10f),new Quaternion());
            Counter = 0;
        }
    }
}
