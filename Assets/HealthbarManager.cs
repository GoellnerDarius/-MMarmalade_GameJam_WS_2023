using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthbarManager : MonoBehaviour
{

    public Slider PlayerHealthBar;
    public Slider BossHealthBar;

    public int PlayerHealth;
    public int BossHealth = 10;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    static void OnBossDamagedEvent()
    {
        
    }

}
