using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class FisherGameController : MonoBehaviour
{
    public static FisherGameController Instance { get; private set; }

    [Header("Game Settings")]
    [SerializeField] float gameDuration = 30f;
    [SerializeField] int pointsPerFish = 100;
    [SerializeField] string highScoreKey = "FishingHighScore";

    [Header("References")]
    [SerializeField] FisherCasting fisherCasting;
    [SerializeField] Text scoreText;
    [SerializeField] Text highScoreText;
    [SerializeField] Text timerText;
    [SerializeField] GameObject gameOverPanel;
    [SerializeField] string lobbySceneName = "Lobby";
    [SerializeField] float returnDelay = 2f;
    int score;
    float timeRemaining;
    bool gameRunning;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    void Start()
    {
        timeRemaining = gameDuration;
        gameRunning = true;

        if (gameOverPanel != null)
            gameOverPanel.SetActive(false);

        score = 0;
        UpdateScoreUI();
        UpdateTimerUI();
        UpdateHighScoreUI();
    }

    void Update()
    {
        if (!gameRunning) return;

        timeRemaining -= Time.deltaTime;
        if (timeRemaining <= 0f)
        {
            timeRemaining = 0f;
            EndGame();
        }

        UpdateTimerUI();
    }


    public void OnFishCaught(int points)
    {
        if (!gameRunning) return;

        score += points;
        UpdateScoreUI();

        int stored = PlayerPrefs.GetInt(highScoreKey, 0);
        if (score > stored)
        {
            PlayerPrefs.SetInt(highScoreKey, score);
            UpdateHighScoreUI();
        }
    }

    void EndGame()
    {
        gameRunning = false;

        // stop casting
        if (fisherCasting != null)
            fisherCasting.enabled = false;


        UpdateTimerUI();

        if (gameOverPanel != null)
            gameOverPanel.SetActive(true);

        StartCoroutine(ReturnToLobby());
    }

    IEnumerator ReturnToLobby()
    {
        yield return new WaitForSeconds(returnDelay);

        SceneManager.LoadScene(lobbySceneName);
    }

    void UpdateScoreUI()
    {
        if (scoreText != null)
            scoreText.text = "Score: " + score;
    }

    void UpdateHighScoreUI()
    {
        if (highScoreText != null)
        {
            int hs = PlayerPrefs.GetInt(highScoreKey, 0);
            highScoreText.text = "High: " + hs;
        }
    }

    void UpdateTimerUI()
    {
        if (timerText != null)
            timerText.text = timeRemaining.ToString("0.0");
    }
}