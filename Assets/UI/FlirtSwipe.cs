using System;
using UnityEngine;
using UnityEngine.UIElements;

public class FlirtSwipe : MonoBehaviour
{
    public event Action<Flame> navigate;

    public Flame[] flames;

    private int currentFlame = 0;
    private UIDocument document;

    //Add logic that interacts with the UI controls in the `OnEnable` methods
    private void OnEnable()
    {
        // The UXML is already instantiated by the UIDocument component
        var uiDocument = GetComponent<UIDocument>();
        document = GetComponent<UIDocument>();

        uiDocument.rootVisualElement.Q<Image>("profile-picture").style.backgroundImage = new StyleBackground(flames[0].Images[1]);
        uiDocument.rootVisualElement.Q<Label>("name").text = flames[0].Name;

        uiDocument.rootVisualElement.Q<Button>("match").RegisterCallback<ClickEvent, VisualElement>(MatchClicked, uiDocument.rootVisualElement);
        uiDocument.rootVisualElement.Q<Button>("skip").RegisterCallback<ClickEvent, VisualElement>(SkipClicked, uiDocument.rootVisualElement);
    }

    private void MatchClicked(ClickEvent evt, VisualElement root)
    {
        navigate.Invoke(flames[currentFlame]);
        Debug.Log("match clicked!");
    }

    private void SkipClicked(ClickEvent evt, VisualElement root)
    {
        if(flames.Length - 1 <= currentFlame)
        {
            currentFlame = 0;
        } else
        {
            currentFlame++;
        }
        UpdateProfile();
        Debug.Log("skip clicked!");
    }

    private void UpdateProfile() {
        document.rootVisualElement.Q<Image>("profile-picture").style.backgroundImage = new StyleBackground(flames[currentFlame].Images[1]);

        document.rootVisualElement.Q<Label>("name").text = flames[currentFlame].Name;
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