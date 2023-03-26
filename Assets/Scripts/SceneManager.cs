using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.WSA;

public class SceneManager : MonoBehaviour
{
    public GameObject Player;
    public GameObject Hot_girl;
    public GameObject Colliders;
    public GameObject ItemGenerator;
    public GameObject ItemMover;
    public GameObject TileMover;
    public GameObject BottomTextbox;

    public float Storydelay = 5;
    public float FadeToGameTime = 5;
    public float FadeToCutsceneTime = 5;
    private float Fadeouttime;
    private float StoryReadTime;
    private float PreStoryTime;
    private bool Gamestarted = false;
    private bool StoryTellState = false;
    private int StoryCount = 0;
    private string[] StoryText = {"a","b","c","d","e","f","g" };
    private string[] Speakers = {"Sam","Ash","Copper"};

    public Text BlackText_center;
    public Text StoryText_bottom;
    public Text Speaker;
    public Image Blackscreen;
    public Image BackgroundImage;
    public Image Dude;
    public Image Girl;
    private Color BlackScreenComponents;
    private Color PeopleColor = new Color(255, 255, 255, 0);
    private Color InvisibleColor = new Color(0, 0, 0, 0);

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
        BottomTextbox.SetActive(false);
        Fadeouttime = FadeToGameTime;
        StoryReadTime = Storydelay;
        PreStoryTime = FadeToCutsceneTime;
        StoryText_bottom.lineSpacing = 4;
    }

    // Update is called once per frame
    void Update()
    {
        if(StoryTellState == false)
        {
            Tellstory();
        }

        if (Gamestarted == false && StoryTellState == true)
        {
            BlackText_center.text = "";
            BottomTextbox.SetActive(false);
            Startgame();
        }
    }
    public void Startgame()
    {
        Fadeouttime -= Time.deltaTime;
        BackgroundImage.color = InvisibleColor;
        Dude.color = InvisibleColor;
        Girl.color = InvisibleColor;
        if (BlackScreenComponents.a > 0 && Fadeouttime < 0)
        {
            BlackScreenComponents.a -= Time.deltaTime;
            Blackscreen.color = BlackScreenComponents;
            // Debug.Log(BlackScreenComponents.a);
            BackgroundImage.color = InvisibleColor;
            Dude.color = InvisibleColor;
            Girl.color = InvisibleColor;
        }
        if (Blackscreen.color.a <= 0)
        {
            Player.SetActive(true);
            Hot_girl.SetActive(true);
            Colliders.SetActive(true);
            ItemGenerator.SetActive(true);
            ItemMover.SetActive(true);
            TileMover.SetActive(true);

            Gamestarted = true;
        }
    }
    public void Tellstory()
    {
        PreStoryTime -= Time.deltaTime;
        if(BackgroundImage.color.a < 1 && PreStoryTime < 0)
        {
            //Debug.Log(BackgroundImage.color.a);
            BlackScreenComponents = BackgroundImage.color;
            BlackScreenComponents.a += 0.5f * Time.deltaTime;
            PeopleColor.a += 0.5f * Time.deltaTime;
            BackgroundImage.color = BlackScreenComponents;
            Girl.color = PeopleColor;
            Dude.color = PeopleColor;
        }
        else
        {
            if(PreStoryTime < 0)
            {
               // Debug.Log("Activate Textbox");
                StoryText_bottom.text = StoryText[StoryCount];
                BottomTextbox.SetActive(true); 
                StoryReadTime -= Time.deltaTime;
                if(StoryReadTime < 0)
                {
                    StoryReadTime = Storydelay;
                    StoryCount++;
                    StoryText_bottom.text = StoryText[StoryCount];
                    if(StoryCount + 1 >= StoryText.Length)
                    {
                        StoryTellState = true;  
                    }
                }
            }
        }
    }
}
