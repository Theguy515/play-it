using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(Collider2D))]
public class Hazard : MonoBehaviour
{
    [SerializeField] float lifetime = 6f;
    Rigidbody2D rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0f;
        rb.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
        GetComponent<Collider2D>().isTrigger = true;
        Destroy(gameObject, lifetime);
    }

    public void Launch(Vector2 velocity) => rb.linearVelocity = velocity;

    public void Freeze()
    {
        rb.linearVelocity = Vector2.zero;
        enabled = false;
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.GetComponent<PlayerQuadMover>() != null)
        {
            var gc = FindFirstObjectByType<GameController>();
            gc?.GameOver();
        }
    }
}
