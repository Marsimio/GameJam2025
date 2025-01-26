using UnityEngine;
using System;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    private float topTime;
    private int playerHealth;
    public float TopTime
    {
        get => topTime;
        set => topTime = Mathf.Max(0, value);
    }

public int PlayerHealth
{
    get => playerHealth;
    private set
    {
        playerHealth = Mathf.Max(0, value);
        OnHealthChanged?.Invoke(playerHealth);
    }
}

    public event Action<int> OnHealthChanged;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public enum GameState
    {
        MainMenu,
        Playing,
        Paused,
        GameOver
    }
    public GameState CurrentState { get; private set; }

    public void ChangeState(GameState newState)
    {
        CurrentState = newState;

        switch (newState)
        {
            case GameState.MainMenu:
                break;
            case GameState.Playing:
                break;
            case GameState.Paused:
                break;
            case GameState.GameOver:
                break;
        }
    }

    public void SetPlayerHealth(int health)
    {
        PlayerHealth = health;
    }

    public void IncreasePlayerHealth()
    {
        PlayerHealth += 1;
    }

    public void DecreasePlayerHealth()
    {
        PlayerHealth -= 1;
    }

    private void Start()
    {
        ChangeState(GameState.MainMenu);
    }
    
}