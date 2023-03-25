using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerShooter : MonoBehaviour
{
    public GameObject bullet;
    private int lifes = 3;
    private float lastShoot;
    private int shield = 3;
    private float pierceTimer = 0;
    private float multishotTimer = 0;

    private int powerupStage = 100;

    // Start is called before the first frame update
    void Start()
    {
        lastShoot = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        //Count down Powerup time
        if (multishotTimer >= 0)
            multishotTimer -= Time.deltaTime;
        if (pierceTimer >= 0)
            pierceTimer -= Time.deltaTime;

        //Handle input
        if (Input.GetKey(KeyCode.Space) && lastShoot + 0.5 < Time.time)
            SpawnBullet();

        if (Input.GetKeyDown(KeyCode.Return))
            DoPowerUp();
    }

    private void DoPowerUp()
    {
        //Shield
        if (powerupStage >= 1)
        {
            shield = 3;
        }

        //Pircing
        if (powerupStage >= 2)
        {
            PowerUpSates.piercing = 3;
            pierceTimer = 10f;
        }

        //Multishot
        if (powerupStage >= 3)
        {
            multishotTimer = 10f;
        }

        powerupStage = 0;
    }

    private void SpawnBullet()
    {
        Instantiate(bullet, transform.position + new Vector3(0, 1.3f, 0), Quaternion.identity);
        lastShoot = Time.time;
        if (multishotTimer > 0)
        {
            Instantiate(bullet, transform.position + new Vector3(0, 1.3f, 0), Quaternion.identity).transform.Rotate(0,0,45);
            Instantiate(bullet, transform.position + new Vector3(0, 1.3f, 0), Quaternion.identity).transform.Rotate(0,0,-45);

        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        //Powerupshield
        if (shield > 0)
        {
            shield--;
            return;
        }

        //Life Calculation
        if (other.tag.Equals("EnemyBullet"))
        {
            lifes--;
            // Destroy(other.gameObject);
        }

        if (lifes == 0)
        {
            Destroy(gameObject);
        }
    }
}