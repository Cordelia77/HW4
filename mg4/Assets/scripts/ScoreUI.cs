using UnityEngine;
using TMPro;

public class ScoreUI : MonoBehaviour
{
    public static ScoreUI Instance;
    public TextMeshProUGUI scoreDisplay;
    public GameObject gameOverPanel;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        GameEvents.OnScoreChanged += UpdateScore;
        GameEvents.OnGameOver += ShowGameOver;
    }

    public void UpdateScore(int newScore)
    {
        if (scoreDisplay != null)
            scoreDisplay.text = newScore.ToString();
    }

    public void ShowGameOver()
    {
        if (gameOverPanel != null) gameOverPanel.SetActive(true);
    }

    void OnDestroy()
    {
        GameEvents.OnScoreChanged -= UpdateScore;
        GameEvents.OnGameOver -= ShowGameOver;
    }
}