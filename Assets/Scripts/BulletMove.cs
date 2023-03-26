using UnityEngine;

public class BulletMove : MonoBehaviour
{
    // Start is called before the first frame update
    public float BulletSpeed = 30;

    private int pierce;
    private GameObject soundeffect;
    void Start()
    {
        pierce = PowerUpSates.piercing;
        soundeffect=GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(new Vector3(0, 1, 0) * (BulletSpeed * Time.deltaTime));
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        Collided(col);
    }

    private void Collided(Collider2D other)
    {
        // Debug.Log(tag + " collides with " + other.tag, gameObject);

        if (CompareTag("EnemyBullet") && !other.CompareTag("EnemyBullet") && !other.CompareTag("Enemy"))
        {
            // Hey moch wos cooles?
        }

        //Reduces Piercing and deletes Bullet
        if (CompareTag("PlayerBullet") && !other.CompareTag("PlayerBullet") && !other.CompareTag("Player"))
        {
            if (other.CompareTag("EnemyBullet"))
            {
                soundeffect.GetComponent<AudioPlayer>().playCustomAudio("Tsch");
            }
            pierce--;
            if (pierce <= 0)
                Destroy(gameObject);
            if (other.CompareTag("EnemyBullet"))
                Destroy(other.gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        Collided(col.collider);
    }
}
