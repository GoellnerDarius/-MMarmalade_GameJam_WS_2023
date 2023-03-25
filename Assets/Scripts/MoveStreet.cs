using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveStreet : MonoBehaviour
{

    void Start()
    {
    }

    void Update()
    {
        gameObject.transform.Translate(new Vector3(0f,50f*Time.deltaTime,0f));
        if (gameObject.transform.position.y >= 105)
        {
            gameObject.transform.position = new Vector3(0f,-105f,0f);
        }
    }
}
