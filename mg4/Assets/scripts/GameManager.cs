using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public bool IsGameOver = false;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI gameOverText;
    private int score = 0;

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
    }

    void Start()
    {
        if (gameOverText != null) gameOverText.gameObject.SetActive(false);
        UpdateScoreUI();
        Time.timeScale = 1;
    }

    public void GameOver()
    {
        IsGameOver = true;
        gameOverText.gameObject.SetActive(true);
        gameOverText.enabled = true;
        GameEvents.TriggerGameOver();
    }

    public void AddScore()
    {
        if (!IsGameOver)
        {
            score++;
            UpdateScoreUI();
            GameEvents.TriggerScoreChanged(score);
        }
    }

    public void OnOnScoreChanged()
    {
        AddScore();
    }

    private void UpdateScoreUI()
    {
        if (scoreText != null) scoreText.text = score.ToString();
    }
}