using UnityEngine;

public class FreeFallPlayerCollision : MonoBehaviour
{
    [SerializeField] private FreeFallGameController gameController;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Obstacle"))
        {
            if (gameController != null)
                gameController.OnPlayerHit();
        }
    }
}