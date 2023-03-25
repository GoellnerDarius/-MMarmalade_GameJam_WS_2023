using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMove : MonoBehaviour
{
    // Start is called before the first frame update
    public float bulletSpeed = 6;
    private int pierce;
    void Start()
    {
        pierce= PowerUpSates.piercing;

    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(new Vector3(0,1,0) * (bulletSpeed * Time.deltaTime));
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        Collided(col);
    }

    private void Collided(Collider2D col)
    {
        //Reduces Piercing and deletes Bullet
        if (!col.tag.Equals("PlayerBullet")&&!col.tag.Equals("Player"))
        {
            pierce--;
            if (pierce <= 0)
                Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        Collided(col.collider);
    }
}
