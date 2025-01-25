using System;
using UnityEngine;
using UnityEngine.UIElements;
using Cursor = UnityEngine.Cursor;

public class MainMenu : MonoBehaviour
{
    private void OnEnable()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        VisualElement root = GetComponent<UIDocument>().rootVisualElement;

        Button buttonStart = root.Q<Button>("Play");
        Button buttonCredits = root.Q<Button>("Credits");
        Button buttonQuit = root.Q<Button>("Quit");
            
        buttonStart.RegisterCallback<MouseEnterEvent>(evt => buttonStart.AddToClassList("buttonHover"));
        buttonStart.RegisterCallback<MouseLeaveEvent>(evt => buttonStart.RemoveFromClassList("buttonHover"));
        buttonStart.clicked += () => UnityEngine.SceneManagement.SceneManager.LoadScene("MainLevel");

        buttonCredits.RegisterCallback<MouseEnterEvent>(evt => buttonCredits.AddToClassList("buttonHover"));
        buttonCredits.RegisterCallback<MouseLeaveEvent>(evt => buttonCredits.RemoveFromClassList("buttonHover"));

        buttonQuit.RegisterCallback<MouseEnterEvent>(evt => buttonQuit.AddToClassList("buttonHover"));
        buttonQuit.RegisterCallback<MouseLeaveEvent>(evt => buttonQuit.RemoveFromClassList("buttonHover"));
        buttonQuit.clicked += () => Application.Quit();
    }

}
