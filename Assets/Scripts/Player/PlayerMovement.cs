using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // Start is called before the first frame update
    public float speed = 10f;
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 movement = getMovement();

        transform.Translate(movement * (speed * Time.deltaTime));
    }



    private Vector3 getMovement()
    {
        Vector3 movement = new(0, 0, 0);

        if (Input.GetKey(KeyCode.A))
            movement += new Vector3(-1, 0, 0);

        if (Input.GetKey(KeyCode.D))
            movement += new Vector3(1, 0, 0);

        if (Input.GetKey(KeyCode.W))
            movement += new Vector3(0, 1, 0);

        if (Input.GetKey(KeyCode.S))
            movement += new Vector3(0, -1, 0);

        return movement;
    }
}