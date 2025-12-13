using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerCarHit : MonoBehaviour
{
    [Header("Scene to load when hit")]
    public string lobbySceneName = "Lobby";

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // If the car has tag "Car"
        if (collision.collider.CompareTag("Car"))
        {
            SceneManager.LoadScene(lobbySceneName);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // If your cars use trigger colliders instead
        if (other.CompareTag("car"))
        {
            SceneManager.LoadScene(lobbySceneName);
        }
    }
}
