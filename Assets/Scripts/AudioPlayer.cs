using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPlayer : MonoBehaviour
{
    public AudioSource DragonDeath;
    public AudioSource ObstacleDeaath;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void playCustomAudio(string sound)
    {
        if(sound.Equals("Dragon"))
            DragonDeath.Play();
        if (sound.Equals("Tsch")&&!ObstacleDeaath.isPlaying)
        {
            ObstacleDeaath.Play();
        }
    }
}
