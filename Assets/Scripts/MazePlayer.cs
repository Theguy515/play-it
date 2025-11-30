using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class MazePlayer : MonoBehaviour
{
    [SerializeField] float moveSpeed = 5f;

    Rigidbody2D rb;
    Vector2 input;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        input.x = Input.GetAxisRaw("Horizontal");
        input.y = Input.GetAxisRaw("Vertical");
        input = input.normalized;
    }

    void FixedUpdate()
    {
        Vector2 targetPos = rb.position + input * moveSpeed * Time.fixedDeltaTime;
        rb.MovePosition(targetPos);
    }
}