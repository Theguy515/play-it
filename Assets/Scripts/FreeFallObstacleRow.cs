using UnityEngine;

public class FreeFallObstacleRow : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 4f;
    [SerializeField] private float destroyY = 10f;

    private void Update()
    {

        transform.Translate(Vector2.up * moveSpeed * Time.deltaTime);

        // Destroy when off screen
        if (transform.position.y > destroyY)
        {
            Destroy(gameObject);
        }
    }
}