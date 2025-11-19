using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using TMPro;

public class GameController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI timerText;
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private FourEdgeSpawner spawner;
    [SerializeField] private PlayerQuadMover playerMover;
    [SerializeField] private float lobbyDelay = 2f;
    private float gameOverTimer = 0f;
    private float timeAlive;
    private bool isGameOver;

    private void Awake()
    {
        if (gameOverPanel) gameOverPanel.SetActive(false);
        timeAlive = 0f;
        isGameOver = false;
    }

    private void Update()
    {
        if (!isGameOver)
        {
            timeAlive += Time.deltaTime;
            if (timerText) timerText.text = timeAlive.ToString("0.00");
        
            if (Keyboard.current == null) return;

            if (Keyboard.current.escapeKey.wasPressedThisFrame)
            SceneManager.LoadScene("Lobby");
       }

       else
        {
            gameOverTimer += Time.deltaTime;

            if(gameOverTimer >= lobbyDelay)
            {
                SceneManager.LoadScene("Lobby");
            }
        }
    }

//gameover function that sets gameover to true and disabled core processes
    public void GameOver()
    {

        if(isGameOver) return;
        isGameOver = true;

        if(spawner)
            spawner.enabled = false;

        if(playerMover)
            playerMover.enabled = false;

        if(gameOverPanel)
        gameOverPanel.SetActive(true);

        gameOverTimer = 0f;
    }
}