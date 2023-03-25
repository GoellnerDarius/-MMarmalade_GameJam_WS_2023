using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthbarManager : MonoBehaviour
{

    public Slider PlayerHealthBar;
    public Slider BossHealthBar;

    public PlayerShooter PlayerHealth;
    public Enemy BossHealth;

    public float PlayerStartingHealth;
    public float BossStartingHealth;

    // Start is called before the first frame update
    void Start()
    {
        PlayerStartingHealth = PlayerHealth.lifes;
        BossStartingHealth = BossHealth.Lifes;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateHealthBar();
        
    }

    public void UpdateHealthBar()
    {
        PlayerHealthBar.value = PlayerHealth.lifes / PlayerStartingHealth;
        BossHealthBar.value = BossHealth.Lifes / BossStartingHealth;
    }

}
