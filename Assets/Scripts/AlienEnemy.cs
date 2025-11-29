using UnityEngine;

public class AlienEnemy : MonoBehaviour
{
    [SerializeField] private float speed = 2f;
    [SerializeField] private int scoreValue = 100;

    private Transform target;

    // Called by the spawner after instantiating this enemy
    public void Initialize(Transform targetTransform)
    {
        target = targetTransform;
    }

    private void Update()
    {
        if (target == null) return;

        // Move toward the target (the player)
        Vector3 dir = (target.position - transform.position).normalized;
        transform.position += dir * speed * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Hit by a bullet
        if (other.CompareTag("Bullet"))
        {
            if (ScoreManager.Instance != null)
            {
                ScoreManager.Instance.AddScore(scoreValue);
            }

            Destroy(other.gameObject);   // remove bullet
            Destroy(gameObject);         // remove alien
            return;
        }

        // Hit the player
        if (other.CompareTag("Player"))
        {
            if (DefenseGameController.Instance != null)
            {
                DefenseGameController.Instance.OnPlayerHit();
            }

            Destroy(gameObject);
        }
    }
}