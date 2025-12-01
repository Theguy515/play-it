using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    void Awake()
    {
        // Singleton setup (only one GameManager exists)
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    // Called when the player hits an obstacle
    public void GameOver()
    {
        // Load the lobby scene immediately
        SceneManager.LoadScene("Lobby");
    }
}
