using System;
using UnityEngine;
using UnityEngine.UIElements;

public class FlirtSwipe : MonoBehaviour
{
    public event Action<Flame> navigate;

    public Flame[] flames;

    private int currentFlame = 0;
    private UIDocument document;

    public bool isStart = true;

    public Texture2D sam;

    //Add logic that interacts with the UI controls in the `OnEnable` methods
    private void OnEnable()
    {
        // The UXML is already instantiated by the UIDocument component
        var uiDocument = GetComponent<UIDocument>();
        document = GetComponent<UIDocument>();
        document.rootVisualElement.Q<Button>("start").RegisterCallback<ClickEvent, VisualElement>(InitProfile, document.rootVisualElement);
        document.rootVisualElement.Q<Image>("profile-picture").style.backgroundImage = new StyleBackground(sam);
        document.rootVisualElement.Q<Image>("profile-picture").style.unityBackgroundScaleMode = ScaleMode.ScaleAndCrop;

        document.rootVisualElement.Q<Label>("name").text = "Sam";
        document.rootVisualElement.Q<Label>("age").text = "Myself";
    }

    public void InitProfile(ClickEvent evt, VisualElement root)
    {
        document.rootVisualElement.Q<Button>("start").style.display = DisplayStyle.None;
        document.rootVisualElement.Q<VisualElement>("skip_match").style.display = DisplayStyle.Flex;

        UpdateProfile();

        document.rootVisualElement.Q<Button>("match").RegisterCallback<ClickEvent, VisualElement>(MatchClicked, document.rootVisualElement);
        document.rootVisualElement.Q<Button>("skip").RegisterCallback<ClickEvent, VisualElement>(SkipClicked, document.rootVisualElement);
    }

    private void MatchClicked(ClickEvent evt, VisualElement root)
    {
        if (flames[currentFlame].isMatch)
        {
            navigate.Invoke(flames[currentFlame]);
            Share.Flame = flames[currentFlame];
        } else
        {
            Skip();
        }
        Debug.Log("match clicked!");
    }

    private void SkipClicked(ClickEvent evt, VisualElement root)
    {
        Skip();
        Debug.Log("skip clicked!");
    }

    private void Skip()
    {
        if (flames.Length - 1 <= currentFlame)
        {
            currentFlame = 0;
        }
        else
        {
            currentFlame++;
        }
        UpdateProfile();
    }

    private void UpdateProfile() {
        document.rootVisualElement.Q<Image>("profile-picture").style.backgroundImage = new StyleBackground(flames[currentFlame].Images[1]);
        document.rootVisualElement.Q<Image>("profile-picture").style.unityBackgroundScaleMode = flames[currentFlame].isMatch ? ScaleMode.ScaleToFit : ScaleMode.ScaleAndCrop;

        document.rootVisualElement.Q<Label>("name").text = flames[currentFlame].Name;
        document.rootVisualElement.Q<Label>("age").text = "Age: " + flames[currentFlame].Age.ToString();
    }

    private void OnDisable()
    {
        // _button.UnregisterCallback<ClickEvent>(PrintClickMessage);
    }

    private void PrintClickMessage(ClickEvent evt)
    {
 
    }

    public static void InputMessage(ChangeEvent<string> evt)
    {
    }
}