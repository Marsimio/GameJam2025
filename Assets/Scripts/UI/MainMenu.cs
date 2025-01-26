using System;
using UnityEngine;
using UnityEngine.UIElements;
using Cursor = UnityEngine.Cursor;

public class MainMenu : MonoBehaviour
{
    private VisualElement root;
    private void OnEnable()
    {
        GameManager.Instance.ChangeState(GameManager.GameState.MainMenu);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        root = GetComponent<UIDocument>().rootVisualElement;
        

        Button buttonStart = root.Q<Button>("Play");
        Button buttonH2P = root.Q<Button>("H2P");
        Button buttonQuit = root.Q<Button>("Quit");
            
        buttonStart.clicked += () =>
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("MainLevel");
            GameManager.Instance.ChangeState(GameManager.GameState.Playing);
        };
        
        
        buttonH2P.clicked += () =>
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("How2");
            GameManager.Instance.ChangeState(GameManager.GameState.Playing);
        };

        buttonQuit.clicked += () => QuitGame();
        

    }

    private void Start()
    {
        Label timerLabel = root.Q<Label>("Timer");
        timerLabel.text = GameManager.Instance.TopTime.ToString();
    }

    void QuitGame()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #elif UNITY_WEBPLAYER
            Application.OpenURL(webplayerQuitURL);
        #else
            Application.Quit();
        #endif
    }
    
}
