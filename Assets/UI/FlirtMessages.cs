using System;
using UnityEngine;
using UnityEngine.UIElements;
using System.IO;

public class FlirtMessages : MonoBehaviour
{
    // public event Action<string> navigate;

    public Flame flame;
    private UIDocument document;

    //Add logic that interacts with the UI controls in the `OnEnable` methods
    private void OnEnable()
    {
        string[] filePaths = Directory.GetFiles("Assets/UI/Dialogs/", "*.json");
        foreach(string path1 in filePaths)
        {
            Debug.Log(path1);
        }
        string path = "Assets/UI/Dialogs/1.json";
        StreamReader inp_stm = new StreamReader(path);
        string json = "";
        while (!inp_stm.EndOfStream)
        {
            json += inp_stm.ReadLine();
            // Do Something with the input. 
        }
        Debug.Log(json);
        Dialog dialog = JsonUtility.FromJson<Dialog>(json);
        Debug.Log(dialog.questions.Length);

        // The UXML is already instantiated by the UIDocument component
        document = GetComponent<UIDocument>();

        document.rootVisualElement.Q<Image>("profile-picture").style.backgroundImage = new StyleBackground(flame.Images[1]);
    }
}

[System.Serializable]
public class Dialog
{
    public Question[] questions;
}

[System.Serializable]
public class Question
{
    public string q;
    public string a;
}