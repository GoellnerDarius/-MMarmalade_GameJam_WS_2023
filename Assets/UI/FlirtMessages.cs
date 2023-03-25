using System;
using UnityEditor.VersionControl;
using UnityEngine;
using UnityEngine.UIElements;

public class FlirtMessages : MonoBehaviour
{
    public event Action<string> navigate;

    public Flame flame;
    private UIDocument document;

    //Add logic that interacts with the UI controls in the `OnEnable` methods
    private void OnEnable()
    {
        // The UXML is already instantiated by the UIDocument component
        document = GetComponent<UIDocument>();

        document.rootVisualElement.Q<Image>("profile-picture").style.backgroundImage = new StyleBackground(flame.Images[1]);
    }
}