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
    }

    public void UpdateScore()
    {

    }

    public void ShowGameOver()
    {
        if (gameOverPanel != null) gameOverPanel.SetActive(true);
    }
}