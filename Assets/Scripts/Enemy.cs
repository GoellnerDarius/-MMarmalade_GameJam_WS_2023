using System;
using System.Collections;
using System.Collections.Generic;
using Unity.PlasticSCM.Editor.WebApi;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // Start is called before the first frame update
    private int lifes = 10;
    private Vector3 leftbounds = new(0, 0, 0);
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag.Equals("PlayerBullet"))
        {
            lifes--;
            if(lifes==0)
                Destroy(gameObject);
        }
    }
}