using UnityEngine;
using UnityEngine.SceneManagement;

public class DefenseGameController : MonoBehaviour
{
    // --- Singleton pattern so other scripts can call DefenseGameController.Instance ---
    public static DefenseGameController Instance { get; private set; }

    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private float returnToLobbyDelay = 2f;
    [SerializeField] private string lobbySceneName = "Lobby"; // change to your actual lobby scene name

    public bool IsGameRunning { get; private set; } = true;

    private void Awake()
    {
        // Basic singleton setup
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    private void Start()
    {
        // Make sure the game starts in a running state
        IsGameRunning = true;

        // Hide the Game Over UI at the start
        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(false);
        }
    }

    // Called by AlienEnemy when it collides with the player
    public void OnPlayerHit()
    {
        if (!IsGameRunning) return;  // avoid double-trigger

        IsGameRunning = false;
        Debug.Log("Defense: GAME OVER");

        // Show game over UI if we have one
        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(true);
        }

        // Optionally stop time so bullets / aliens freeze
        Time.timeScale = 0f;

        // Start coroutine to return to lobby after a short delay in *real* time
        StartCoroutine(ReturnToLobbyAfterDelay());
    }

    private System.Collections.IEnumerator ReturnToLobbyAfterDelay()
    {
        // Wait using unscaled time because we set Time.timeScale = 0
        float elapsed = 0f;
        while (elapsed < returnToLobbyDelay)
        {
            elapsed += Time.unscaledDeltaTime;
            yield return null;
        }

        Time.timeScale = 1f; // restore timescale before loading a new scene
        SceneManager.LoadScene(lobbySceneName);
    }
}