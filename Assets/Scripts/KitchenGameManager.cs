using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KitchenGameManager : MonoBehaviour
{
    public event EventHandler OnStateChanged;

    public static KitchenGameManager Instance { get; private set; }
    public enum GameState
    {
        WaitingToStart,
        CountDownToStart,
        GamePlaying,
        GameOver
    }

    private GameState gameState = GameState.WaitingToStart;
    public bool IsGamePlaying => gameState == GameState.GamePlaying;
    public bool IsCountdownToStartActive => gameState == GameState.CountDownToStart;
    public bool IsGameEnd => gameState == GameState.GameOver;

    [SerializeField] private float waitingToStartTimer = 1f;
    [SerializeField] private float countDownToStartTimer = 3f;
    [SerializeField] private float gamePlayingTimer = 60f;

    private void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
        switch (gameState)
        {
            case GameState.WaitingToStart:
                waitingToStartTimer -= Time.deltaTime;
                if (waitingToStartTimer < 0)
                {
                    gameState = GameState.CountDownToStart;
                    OnStateChanged?.Invoke(this, EventArgs.Empty);
                }
                break;
            case GameState.CountDownToStart:
                countDownToStartTimer -= Time.deltaTime;
                if (countDownToStartTimer < 0)
                {
                    gameState = GameState.GamePlaying;
                    OnStateChanged?.Invoke(this, EventArgs.Empty);
                }
                break;
            case GameState.GamePlaying:
                gamePlayingTimer -= Time.deltaTime;
                if (gamePlayingTimer < 0)
                {
                    gameState = GameState.GameOver;
                    OnStateChanged?.Invoke(this, EventArgs.Empty);

                }
                break;
            case GameState.GameOver:
                break;
        }
    }

    public float GetCountdownToStartTimer()
    {
        return countDownToStartTimer;
    }
}
