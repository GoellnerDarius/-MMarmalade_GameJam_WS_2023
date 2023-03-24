using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRotation : MonoBehaviour
{
    // Start is called before the first frame update
    public float rotation = 6;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(getRotation()*rotation);

    }
    private Vector3 getRotation()
    {
        Vector3 rotation = new();
        if (Input.GetKeyDown(KeyCode.A))
            rotation += new Vector3(0, 0, 1);
        if (Input.GetKeyDown(KeyCode.D))
            rotation += new Vector3(0, 0, -1);
        if (Input.GetKeyUp(KeyCode.A))
            rotation -= new Vector3(0, 0, 1);
        if (Input.GetKeyUp(KeyCode.D))
            rotation -= new Vector3(0, 0, -1);
        return rotation;
    }
}
