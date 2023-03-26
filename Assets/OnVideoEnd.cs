using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class OnVideoEnd : MonoBehaviour
{
    public VideoPlayer VideoPlayer;

    void Start()
    {
        VideoPlayer.loopPointReached += (vp) =>
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("UIScene");
        };
    }
}
