using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // Start is called before the first frame update
    public float speed = 10f;
    public float upperBound =  0;
    public float lowerBounds = -9;
    public float leftBound= -17;
    public float rightBound = 17;
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
        float moveSpeed = (speed * Time.deltaTime);
        Vector3 movement = new(0, 0, 0);

        if (Input.GetKey(KeyCode.A)&&transform.position.x-moveSpeed>leftBound)
            movement += new Vector3(-1, 0, 0);

        if (Input.GetKey(KeyCode.D)&&transform.position.x+moveSpeed<rightBound)
            movement += new Vector3(1, 0, 0);

        if (Input.GetKey(KeyCode.W)&&transform.position.y+moveSpeed<upperBound)
            movement += new Vector3(0, 1, 0);

        if (Input.GetKey(KeyCode.S)&&transform.position.y-moveSpeed>lowerBounds)
            movement += new Vector3(0, -1, 0);

        return movement;
    }
}