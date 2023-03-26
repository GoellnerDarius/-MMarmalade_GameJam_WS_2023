using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerShooter : MonoBehaviour
{
    public SpriteRenderer Sprite;
    public GameObject bullet;
    public int lifes = 3;
    private float lastShoot;
    private int shield = 0;
    private float pierceTimer = 0;
    private float multishotTimer = 0;
    public int selection = 0;
    public Vector3 iconScale = new(2, 2, 2);
    public GameObject[] powerupIcons = new GameObject[3];
    
// Start is called before the first frame update
    void Start()
    {
        if (Share.Score >= 110)
            PowerUpSates.stage1 = true;
        if (Share.Score >= 130)
            PowerUpSates.stage2 = true;
        if (Share.Score >= 150)
            PowerUpSates.stage3 = true;
        lastShoot = Time.time;

        // PowerUpSates.stage1 = true;
        // PowerUpSates.stage2 = true;
        // PowerUpSates.stage3 = true;

        powerupIcons[0].SetActive(PowerUpSates.stage1);
        powerupIcons[1].SetActive(PowerUpSates.stage2);
        powerupIcons[2].SetActive(PowerUpSates.stage3);
    }

    // Update is called once per frame
    void Update()
    {
        //Count down Powerup time
        if (multishotTimer >= 0)
            multishotTimer -= Time.deltaTime;
        if (pierceTimer >= 0)
        {
            pierceTimer -= Time.deltaTime;
            if (pierceTimer <= 0)
                PowerUpSates.piercing = 1;
        }

        //Handle input
        if (Input.GetKey(KeyCode.Space) && lastShoot + 0.5 < Time.time)
            SpawnBullet();

        if (Input.GetKeyDown(KeyCode.Return))
            DoPowerUp();
        selectPowerup();
    }


    private void DoPowerUp()

    {

        if (PowerUpSates.stage1 && selection == 0)
        {
            shield = 3;
            PowerUpSates.stage1 = false;
        }

        //Pircing
        if (PowerUpSates.stage1 && selection == 1)
        {
            PowerUpSates.piercing = 3;
            pierceTimer = 10f;
            PowerUpSates.stage2 = false;
        }

        //Multishot
        if (PowerUpSates.stage1 && selection == 2)
        {
            multishotTimer = 10f;
            PowerUpSates.stage3 = false;
        }
    }
    private void selectPowerup()
    {
        powerupIcons[0].SetActive(PowerUpSates.stage1);
        powerupIcons[1].SetActive(PowerUpSates.stage2);
        powerupIcons[2].SetActive(PowerUpSates.stage3);

        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            powerupIcons[selection].transform.localScale = new Vector3(1, 1, 1);
            selection--;
            if (selection == -1)
                selection = 2;
            if (!powerupIcons[selection].activeSelf)
            {
                selection--;
                if (selection == -1)
                    selection = 2;
                
            }
            if (!powerupIcons[selection].activeSelf)
            {
                selection--;
                if (selection == -1)
                    selection = 2;
                
            }
            powerupIcons[selection].transform.localScale = iconScale;
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            powerupIcons[selection].transform.localScale = new Vector3(1, 1, 1);
            selection++;
            if (selection == 3)
                selection = 0;
            if (!powerupIcons[selection].activeSelf)
            {
                selection++;
                if (selection == 3)
                    selection = 0;
                
            }
            if (!powerupIcons[selection].activeSelf)
            {
                selection++;
                if (selection == 3)
                    selection = 0;
                
            }
            powerupIcons[selection].transform.localScale = iconScale;
        }
    }

    private void SpawnBullet()
    {
        Vector3 position = transform.position;
        Instantiate(bullet, position + new Vector3(0, 2.3f, 0), Quaternion.identity);
        lastShoot = Time.time;
        if (multishotTimer > 0)
        {
            Instantiate(bullet, position + new Vector3(0, 2.3f, 0), Quaternion.identity).transform.Rotate(0, 0, 45);
            Instantiate(bullet, position + new Vector3(0, 2.3f, 0), Quaternion.identity).transform.Rotate(0, 0, -45);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Collide(other);
    }

    private void Collide(Collider2D other)
    {
        //Powerupshield
        if (shield > 0)
        {
            shield--;
            Destroy(other.gameObject);

            return;
        }

        //Life Calculation
        if (other.tag.Equals("EnemyBullet"))
        {
            StartCoroutine(HitEffect());
            lifes--;
            Destroy(other.gameObject);
        }

        if (lifes == 0)
        {
            Destroy(gameObject);
        }
    }


    private IEnumerator HitEffect()
    {
        int howManyFlashes = 5;

        while (howManyFlashes > 0)
        {
            Sprite.color = Color.black;
            yield return new WaitForSeconds(.1f);
            Sprite.color = Color.white;
            yield return new WaitForSeconds(.1f);
            howManyFlashes--;
        }
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        Collide(col.collider);
    }
}