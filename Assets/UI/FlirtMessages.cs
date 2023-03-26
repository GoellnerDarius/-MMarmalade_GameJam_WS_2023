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

    public int temperature = 50;

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

        // INIT UI
        document.rootVisualElement.Q<Image>("profile-picture").style.backgroundImage = new StyleBackground(flame.Images[1]);
        document.rootVisualElement.Q<ProgressBar>("temperature").value = temperature;

        SetNextDialog();
    }

    public void SetNextDialog()
    {
        if (prevDialogs.Count < dialogs.Count)
        {
            do
            {
                currentDialog = dialogs[random.Next(dialogs.Count)];
            } while (prevDialogs.Contains(currentDialog));
            prevDialogs.Add(currentDialog);
            AddQuestion(currentDialog.q);
            UpdateAnswers(currentDialog.answers);
        }
        else
        {
            // TODO: Start minigame with score
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

    public void UpdateAnswers(Answer[] answers)
    {
        document.rootVisualElement.Q<Button>("a1").UnregisterCallback<ClickEvent, Answer>(AnswerClicked);
        document.rootVisualElement.Q<Button>("a2").UnregisterCallback<ClickEvent, Answer>(AnswerClicked);
        document.rootVisualElement.Q<Button>("a3").UnregisterCallback<ClickEvent, Answer>(AnswerClicked);
        random.Shuffle(answers);
        document.rootVisualElement.Q<Button>("a1").text = answers[0].a;
        document.rootVisualElement.Q<Button>("a1").RegisterCallback<ClickEvent, Answer>(AnswerClicked, answers[0]);
        document.rootVisualElement.Q<Button>("a2").text = answers[1].a;
        document.rootVisualElement.Q<Button>("a2").RegisterCallback<ClickEvent, Answer> (AnswerClicked, answers[1]);
        document.rootVisualElement.Q<Button>("a3").text = answers[2].a;
        document.rootVisualElement.Q<Button>("a3").RegisterCallback<ClickEvent, Answer>(AnswerClicked, answers[2]);
    }

    private void AnswerClicked(ClickEvent evt, Answer answer)
    {
        AddAnswer(answer.a);
        if(answer.mood == 1 || answer.mood == 0)
        {
            if(answer.mood == 0 && answer.followup != null) {
                temperature += 5;
            } else { 
                temperature += 10;
            }
        } else
        {
            temperature -= 10;
        }
        UpdateTemperature();
        if (answer.mood == 1 && answer.followup != null && answer.followup.q != null)
        {
            Debug.Log(answer.followup);
            Debug.Log(answer.followup.q);
            AddQuestion(answer.followup.q);
            UpdateAnswers(answer.followup.answers);
        } else
        {
            SetNextDialog();
        }
    }

    private void UpdateTemperature()
    {
        Debug.Log("Temperature:" + temperature);
        document.rootVisualElement.Q<ProgressBar>("temperature").value = temperature >= 100 ? 99 : (temperature <= 0 ? 1 : temperature);
        // TODO FIX values over 100 and under 0 because the progress bar isnt properly filled
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
    public Dialog followup;
}