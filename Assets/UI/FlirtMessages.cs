using System;
using UnityEngine;
using UnityEngine.UIElements;
using System.IO;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine.Tilemaps;

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

    public bool isFollowup = false;

    public Label typingIndicator;

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
        ChangeFace(Reaction.neutral);
        document.rootVisualElement.Q<ProgressBar>("temperature").value = temperature;
        document.rootVisualElement.Q<VisualElement>("bubbles").style.opacity = 0;

        SetTempImage("temp.png");

        SetNextDialog();
    }

    public async void SetNextDialog()
    {
        if (prevDialogs.Count < dialogs.Count)
        {
            do
            {
                currentDialog = dialogs[random.Next(dialogs.Count)];
            } while (prevDialogs.Contains(currentDialog));
            prevDialogs.Add(currentDialog);
            await AddQuestion(currentDialog.q);
            ChangeFace(Reaction.neutral);
            UpdateAnswers(currentDialog.answers);
        }
        else
        {
            if (temperature >= 100)
            {
                Share.Score = temperature;
                await AddQuestion("Treffen wir uns?");
                await WaitSecondsAsync(2);
                AddAnswer("Ja klar!");
                await WaitSecondsAsync(1);
                document.rootVisualElement.Q<VisualElement>("overlay").style.display = DisplayStyle.Flex;
                document.rootVisualElement.Q<VisualElement>("overlay").style.opacity = 1;
                await WaitSecondsAsync(2);
                UnityEngine.SceneManagement.SceneManager.LoadScene("Goellner_Test");
            }
            else
            {
                await WaitSecondsAsync(1);
                document.rootVisualElement.Q<VisualElement>("overlay").style.display = DisplayStyle.Flex;
                document.rootVisualElement.Q<VisualElement>("overlay").style.opacity = 1;
                await WaitSecondsAsync(2);
                UnityEngine.SceneManagement.SceneManager.LoadScene("EndScene");
            }
        }
    }

    public async Task AddQuestion(string question)
    {
        Label label = new Label();
        label.AddToClassList("left");
        label.text = question;
        AnimateTyping();
        await WaitSecondsAsync(3);
        typingIndicator.style.display = DisplayStyle.None;
        document.rootVisualElement.Q<VisualElement>("messages").Insert(2, label);
    }

    private void AnimateTyping()
    {
        typingIndicator = document.rootVisualElement.Q<Label>("typing-indicator");
        typingIndicator.style.display = DisplayStyle.Flex;
        typingIndicator.style.opacity = 1;
        typingIndicator.Query(className: "circle")
                     .ForEach(async (element) =>
                     {
                         while (typingIndicator.style.display == DisplayStyle.Flex)
                         {
                             element.style.opacity = 0.2f;
                             await WaitSecondsAsync(1f);
                             element.style.opacity = 0.5f;
                             await WaitSecondsAsync(1f);
                         }
                     });
    }

    private async Task WaitSecondsAsync(float seconds)
    {
        await Task.Delay(TimeSpan.FromSeconds(seconds));
    }

    public void AddAnswer(string answer)
    {
        Label label = new Label();
        label.AddToClassList("right");
        label.text = answer;
        document.rootVisualElement.Q<VisualElement>("messages").Insert(2, label);
    }

    public void UpdateAnswers(Answer[] answers)
    {
        document.rootVisualElement.Q<VisualElement>("bubbles").style.opacity = 1;
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

    private async void AnswerClicked(ClickEvent evt, Answer answer)
    {
        document.rootVisualElement.Q<VisualElement>("bubbles").style.opacity = 0;
        AddAnswer(answer.a);
        if(answer.mood == 1 || answer.mood == 0)
        {
            if (answer.mood == 0 && isFollowup) {
                temperature += 5;
            } else { 
                temperature += 10;
                if(answer.mood == 1) {
                    ChangeFace(Reaction.happy);
                }
            }
        } else
        {
            temperature -= 10;
            ChangeFace(Reaction.angry);
            await WaitSecondsAsync(2);
            ChangeFace(Reaction.neutral);
        }
        UpdateTemperature();
        if (answer.mood == 1 && answer.followup != null && answer.followup.q != null)
        {
            isFollowup = true;
            await AddQuestion(answer.followup.q);
            ChangeFace(Reaction.neutral);
            UpdateAnswers(answer.followup.answers);
        } else
        {
            isFollowup = false;
            SetNextDialog();
        }
    }

    private void UpdateTemperature()
    {
        document.rootVisualElement.Q<ProgressBar>("temperature").value = temperature >= 100 ? 99 : (temperature <= 0 ? 1 : temperature);
        if (temperature <= 100) {
            SetTempImage("temp.png");
        } else
        {
            SetTempImage("temp_max.png");
        }
    }

    private void SetTempImage(string filename)
    {
        byte[] fileData;
        fileData = File.ReadAllBytes("Assets/UI/Images/" + filename);
        Texture2D tex = new Texture2D(2, 2);
        tex.LoadImage(fileData); //..this will auto-resize the texture dimensions.
        document.rootVisualElement.Q<IMGUIContainer>("temp").style.backgroundImage = tex;
    }

    private void ChangeFace(Reaction reaction)
    {
        int index = 0;
        switch(reaction)
        {
            case Reaction.happy:
                index = 0;
                break;
            case Reaction.neutral:
                index = 1;
                break;
            case Reaction.angry:
                index = 2;
                break;
        }
        document.rootVisualElement.Q<Image>("profile-picture").style.backgroundImage = new StyleBackground(flame.Images[index]);

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

public enum Reaction
{
    angry,
    neutral,
    happy
}