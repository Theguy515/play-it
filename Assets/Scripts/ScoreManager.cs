using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance { get; private set; }

    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] TextMeshProUGUI highScoreText;

    [Header("Per-game settings")]
    [SerializeField] string highScoreKey = "HighScore_Default"; // <-- NEW

    int currentScore;
    int highScore;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;

        // Load the high score for THIS game
        highScore = PlayerPrefs.GetInt(highScoreKey, 0);
        UpdateUI();
    }

    public void AddScore(int amount)
    {
        currentScore += amount;

        if (currentScore > highScore)
        {
            highScore = currentScore;
            PlayerPrefs.SetInt(highScoreKey, highScore);   // <-- use per-game key
            PlayerPrefs.Save();
        }

        UpdateUI();
    }

    public void ResetScore()
    {
        currentScore = 0;
        UpdateUI();
    }

    void UpdateUI()
    {
        if (scoreText != null)
            scoreText.text = $"Score: {currentScore}";

        if (highScoreText != null)
            highScoreText.text = $"High: {highScore}";
    }
}