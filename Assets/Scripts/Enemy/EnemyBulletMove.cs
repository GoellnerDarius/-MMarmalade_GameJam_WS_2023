using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBulletMove : MonoBehaviour
{
    public float bulletSpeed = 6;
    

    // Update is called once per frame
    void Update()
    {
        transform.Translate(new Vector3(0, 1, 0) * (bulletSpeed * Time.deltaTime));
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (!col.gameObject.tag.Equals("Enemy") || !col.gameObject.tag.Equals("EnemyBullet"))
        {
            Destroy(gameObject);
        }
    }
}