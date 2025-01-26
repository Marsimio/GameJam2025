using UnityEngine;
using UnityEngine.UIElements;
using Cursor = UnityEngine.Cursor;

public class GameUI : MonoBehaviour
{
    private VisualElement healthBubbles;

    private PlayerInput _playerInput;
    private PlayerInput.PlayerAbilitiesActions _playerAbilitiesActions;

    private VisualElement root;
    private VisualElement pause;
    private Label timerLabel;
    private AudioSource audioSource;
    private float gameTime;

    void Awake()
    {
        _playerInput = new PlayerInput();
        _playerAbilitiesActions = _playerInput.PlayerAbilities;
        audioSource = GameObject.FindWithTag("Audio")?.GetComponent<AudioSource>();

        _playerAbilitiesActions.Pause.performed += _ =>
        {
            if (GameManager.Instance.CurrentState == GameManager.GameState.Playing)
            {
                PauseGame();
            }
            else if (GameManager.Instance.CurrentState == GameManager.GameState.Paused)
            {
                ResumeGame();
            }
        };
    }

    private void OnEnable()
    {
        root = GetComponent<UIDocument>().rootVisualElement;
        pause = root.Q<VisualElement>("Pause");
        timerLabel = root.Q<Label>("Timer");
        _playerAbilitiesActions.Enable();
    }

    private void Update()
    {
        if (GameManager.Instance.CurrentState == GameManager.GameState.Playing)
        {
            gameTime += Time.deltaTime;
            if (timerLabel != null)
            {
                timerLabel.text = $"Timer: {gameTime:F2}";
            }
        }
            
        if (GameManager.Instance.CurrentState == GameManager.GameState.GameOver)
        {
            if (timerLabel != null)
            {
                string[] labelParts = timerLabel.text.Split(' ');
                if (labelParts.Length > 1 && float.TryParse(labelParts[1], out float parsedTime))
                {
                    GameManager.Instance.TopTime = parsedTime;
                }
            }
        }
    }

    private void OnDisable()
    {
        _playerAbilitiesActions.Disable();
    }

    public void PauseGame()
    {
        if (GameManager.Instance.CurrentState == GameManager.GameState.Playing)
        {
            GameManager.Instance.ChangeState(GameManager.GameState.Paused);
            Time.timeScale = 0f;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;

            pause.visible = true;

            if (audioSource != null)
            {
                audioSource.Pause();
            }
        }
    }

    public void ResumeGame()
    {
        if (GameManager.Instance.CurrentState == GameManager.GameState.Paused)
        {
            GameManager.Instance.ChangeState(GameManager.GameState.Playing);
            Time.timeScale = 1f;
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;

            pause.visible = false;

            if (audioSource != null)
            {
                audioSource.UnPause();
            }
        }
    }

}