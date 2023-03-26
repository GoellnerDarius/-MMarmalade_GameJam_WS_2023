using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.UI;

public class SceneManager : MonoBehaviour
{
    public GameObject Player;
    public GameObject Hot_girl;
    public GameObject Colliders;
    public GameObject ItemGenerator;
    public GameObject ItemMover;
    public GameObject TileMover;

    public float Fadeouttime = 4;
    private bool Gamestarted = false;

    public Text BlackText;
    public Image Blackscreen;
    private Color BlackScreenComponents;

    // Start is called before the first frame update
    void Start()
    {
        BlackScreenComponents = Blackscreen.color;
        Player.SetActive(false);
        Hot_girl.SetActive(false);
        Colliders.SetActive(false);
        ItemGenerator.SetActive(false);
        ItemMover.SetActive(false);
        TileMover.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Gamestarted == false)
        {
            Fadeouttime -= Time.deltaTime;
            if (BlackScreenComponents.a > 0 && Fadeouttime < 0)
            {
                BlackScreenComponents.a -= 5 * Time.deltaTime;
                Blackscreen.color = BlackScreenComponents;
                Debug.Log(BlackScreenComponents.a);
            }
            if (Blackscreen.color.a <= 0)
            {
                Player.SetActive(true);
                Hot_girl.SetActive(true);
                Colliders.SetActive(true);
                ItemGenerator.SetActive(true);
                ItemMover.SetActive(true);
                TileMover.SetActive(true);
                BlackText.text = "";
                Gamestarted = true;
            }
        }
    }
}
