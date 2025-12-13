using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FreeFallGameController : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private Text scoreText;
    [SerializeField] private Text highScoreText;
    [SerializeField] private GameObject gameOverPanel;

    [Header("Scoring")]
    [SerializeField] private float scorePerSecond = 10f;
    [SerializeField] private string highScoreKey = "FreeFallHighScore";

    [Header("Scene")]
    [SerializeField] private string lobbySceneName = "Lobby";   // or whatever your lobby scene is called
    [SerializeField] private float returnToLobbyDelay = 2f;

    private float currentScore;
    private bool isGameOver;

    private void Start()
    {
        currentScore = 0f;
        isGameOver = false;

        if (gameOverPanel != null)
            gameOverPanel.SetActive(false);

        int savedHigh = PlayerPrefs.GetInt(highScoreKey, 0);
        if (highScoreText != null)
            highScoreText.text = $"High: {savedHigh}";
    }

    private void Update()
    {
        if (isGameOver) return;

        // Add score over time
        currentScore += scorePerSecond * Time.deltaTime;

        if (scoreText != null)
            scoreText.text = $"Score: {(int)currentScore}";
    }

    public void OnPlayerHit()
    {
        if (isGameOver) return;
        isGameOver = true;

        if (gameOverPanel != null)
            gameOverPanel.SetActive(true);

        // High score
        int savedHigh = PlayerPrefs.GetInt(highScoreKey, 0);
        if (currentScore > savedHigh)
        {
            savedHigh = (int)currentScore;
            PlayerPrefs.SetInt(highScoreKey, savedHigh);
            PlayerPrefs.Save();
        }

        if (highScoreText != null)
            highScoreText.text = $"High: {savedHigh}";

        StartCoroutine(ReturnToLobby());
    }

    private IEnumerator ReturnToLobby()
    {
        yield return new WaitForSeconds(returnToLobbyDelay);
        SceneManager.LoadScene(lobbySceneName);
    }
}