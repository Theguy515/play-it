using UnityEngine;

public class ObstacleMover : MonoBehaviour
{
    public float speed = 6f;

    void Update()
    {
        // Move left every frame
        transform.position += Vector3.left * speed * Time.deltaTime;

        // Destroy when off-screen on the left
        if (transform.position.x < -15f)
        {
            Destroy(gameObject);
        }
    }
}
