using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiManager : MonoBehaviour
{
    public FlirtSwipe flirtSwipe;
    public FlirtMessages flirtMessages;

    // Start is called before the first frame update
    void Start()
    {
        flirtSwipe.navigate += NavigateToMessages;
    }


    void NavigateToMessages(Flame flame)
    {
        flirtSwipe.gameObject.SetActive(false);
        flirtMessages.flame = flame;
        flirtMessages.gameObject.SetActive(true);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
