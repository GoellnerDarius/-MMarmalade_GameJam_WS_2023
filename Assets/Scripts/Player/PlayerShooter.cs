using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooter : MonoBehaviour
{
    public GameObject bullet;

    private float lastShoot;
    // Start is called before the first frame update
    void Start()
    {
        lastShoot = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Space)&&lastShoot+0.5<Time.time)
        {
            Instantiate(bullet, transform.position+new Vector3(0,1.3f,0), Quaternion.identity);
            lastShoot = Time.time;

        }
    }
}
