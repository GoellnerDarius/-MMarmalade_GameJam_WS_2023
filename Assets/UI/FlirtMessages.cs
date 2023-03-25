using System;
using UnityEngine;
using UnityEngine.UIElements;
using System.IO;
using System.Collections.Generic;

public class FlirtMessages : MonoBehaviour
{
    // public event Action<string> navigate;

    public Flame flame;
    private UIDocument document;

    // all available characters 
    public List<Dialog> dialogs = new List<Dialog>();

    public Dialog currentDialog;

    // saves previous dialogs to prevent duplicate questions
    public List<Dialog> prevDialogs = new List<Dialog>();

    public System.Random random = new System.Random();

    //Add logic that interacts with the UI controls in the `OnEnable` methods
    private void OnEnable()
    {
        string[] filePaths = Directory.GetFiles("Assets/UI/Dialogs/" + flame.Name, "*.json");
        foreach (string path in filePaths)
        {
            StreamReader reader = new StreamReader(path);
            string json = "";
            while (!reader.EndOfStream)
            {
                json += reader.ReadLine();
            }
            dialogs.Add(JsonUtility.FromJson<Dialog>(json));
        }

        document = GetComponent<UIDocument>();

        document.rootVisualElement.Q<Image>("profile-picture").style.backgroundImage = new StyleBackground(flame.Images[1]);

        SetNextDialog();
        /*string path = "Assets/UI/Dialogs/1.json";
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
        */
        // The UXML is already instantiated by the UIDocument component

    }

    public void SetNextDialog()
    {
        if (prevDialogs.Count < dialogs.Count)
        {
            do
            {
                currentDialog = dialogs[random.Next(dialogs.Count)];
            } while (prevDialogs.Contains(currentDialog));
            AddQuestion(currentDialog.q);
            UpdateAnswers();
        }
        else
        {
            Application.Quit();
        }
    }

    public void AddQuestion(string question)
    {
        Label label = new Label();
        label.AddToClassList("left");
        label.text = question;
        document.rootVisualElement.Q<VisualElement>("messages").Insert(0, label);
    }

    public void AddAnswer(string answer)
    {
        Label label = new Label();
        label.AddToClassList("right");
        label.text = answer;
        document.rootVisualElement.Q<VisualElement>("messages").Insert(0, label);
    }

    public void UpdateAnswers()
    {
        random.Shuffle(currentDialog.answers);
        document.rootVisualElement.Q<Button>("a1").text = currentDialog.answers[0].a;
        document.rootVisualElement.Q<Button>("a1").RegisterCallback<ClickEvent, int>(AnswerClicked, 0);
        document.rootVisualElement.Q<Button>("a2").text = currentDialog.answers[1].a;
        document.rootVisualElement.Q<Button>("a2").RegisterCallback<ClickEvent, int>(AnswerClicked, 1);
        document.rootVisualElement.Q<Button>("a3").text = currentDialog.answers[2].a;
        document.rootVisualElement.Q<Button>("a3").RegisterCallback<ClickEvent, int>(AnswerClicked, 2);
    }

    private void AnswerClicked(ClickEvent evt, int index)
    {
        AddAnswer(currentDialog.answers[index].a);
        if (currentDialog.answers[index].mood == 1)
        {
            // AddQuestion(currentDialog.answers[index])
        }
    }
}
[System.Serializable]
public class Dialog
{
    public string q;
    public Answer[] answers;
}

[System.Serializable]
public class Answer
{
    public string a;
    public int mood;
    // public Dialog followup;
}