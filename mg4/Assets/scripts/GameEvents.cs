using UnityEngine;
using System;

public static class GameEvents
{
    public static event Action<int> OnScoreChanged;
    public static event Action OnGameOver;
    public static event Action OnPlayerFlap;

    public static void TriggerScoreChanged(int newScore)
    {
        OnScoreChanged?.Invoke(newScore);
    }

    public static void TriggerGameOver()
    {
        OnGameOver?.Invoke();
    }

    public static void TriggerPlayerFlap()
    {
        OnPlayerFlap?.Invoke();
    }
}