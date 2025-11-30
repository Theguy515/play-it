using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MazeGameController : MonoBehaviour
{
    public static MazeGameController Instance { get; private set; }

    [Header("Settings")]
    [SerializeField] string lobbySceneName = "Lobby";
    [SerializeField] float returnDelay = 2f;
    [SerializeField] string bestTimeKey = "MazeBestTime";

    [Header("UI")]
    [SerializeField] TMP_Text timeText;
    [SerializeField] TMP_Text bestTimeText;
    [SerializeField] GameObject gameOverPanel;

    float elapsedTime;
    bool running = true;
    float bestTime = -1f;

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
        if (gameOverPanel != null)
            gameOverPanel.SetActive(false);

        bestTime = PlayerPrefs.GetFloat(bestTimeKey, -1f);
        UpdateBestTimeUI();
    }

    void Update()
    {
        if (!running) return;

        elapsedTime += Time.deltaTime;
        UpdateTimeUI();
    }

    void UpdateTimeUI()
    {
        if (timeText != null)
            timeText.text = elapsedTime.ToString("0.00") + "s";
    }

    void UpdateBestTimeUI()
    {
        if (bestTimeText != null)
        {
            if (bestTime < 0)
                bestTimeText.text = "Best: --";
            else
                bestTimeText.text = "Best: " + bestTime.ToString("0.00") + "s";
        }
    }

    public void OnExitReached()
    {
        if (!running) return;

        running = false;

        // check best time
        if (bestTime < 0 || elapsedTime < bestTime)
        {
            bestTime = elapsedTime;
            PlayerPrefs.SetFloat(bestTimeKey, bestTime);
            UpdateBestTimeUI();
        }

        if (gameOverPanel != null)
            gameOverPanel.SetActive(true);

        StartCoroutine(ReturnToLobby());
    }

    IEnumerator ReturnToLobby()
    {
        yield return new WaitForSeconds(returnDelay);
        SceneManager.LoadScene(lobbySceneName);
    }
}