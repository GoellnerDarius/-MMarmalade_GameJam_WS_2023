using UnityEditor.VersionControl;
using UnityEngine;
using UnityEngine.UIElements;

public class FlirtSwipe : MonoBehaviour
{
    private Button _button;
    //private Toggle _toggle;

    //Add logic that interacts with the UI controls in the `OnEnable` methods
    private void OnEnable()
    {
        // The UXML is already instantiated by the UIDocument component
        var uiDocument = GetComponent<UIDocument>();
        
        _button = uiDocument.rootVisualElement.Q<Button>("match");
        _button.RegisterCallback<ClickEvent, VisualElement>(MatchClicked, uiDocument.rootVisualElement);

        _button = uiDocument.rootVisualElement.Q<Button>("skip");
        _button.RegisterCallback<ClickEvent, VisualElement>(SkipClicked, uiDocument.rootVisualElement);

        // TODO: Create one Base FlirtScreen cs File and Load the other views dynamically.
        // _button.UnregisterCallback<ClickEvent>(SkipClicked);
        /*_toggle = uiDocument.rootVisualElement.Q("toggle") as Toggle;

        _button.RegisterCallback<ClickEvent>(PrintClickMessage);

        var _inputFields = uiDocument.rootVisualElement.Q("input-message");
        _inputFields.RegisterCallback<ChangeEvent<string>>(InputMessage);*/
    }

    private void MatchClicked(ClickEvent evt, VisualElement root)
    {

        Debug.Log("match clicked!");
    }

    private void SkipClicked(ClickEvent evt, VisualElement root)
    {
        Debug.Log("skip clicked!");
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