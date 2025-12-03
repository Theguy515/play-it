using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class FreeFallPlayer : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 7f;
    [SerializeField] private float xLimit = 4f;  // how far left/right player can go

    private Rigidbody2D rb;
    private float inputX;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        inputX = Input.GetAxisRaw("Horizontal"); // A/D or left/right arrows
    }

    private void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(inputX * moveSpeed, 0f);

        // Clamp position so player can't leave the screen
        Vector2 pos = rb.position;
        pos.x = Mathf.Clamp(pos.x, -xLimit, xLimit);
        rb.position = pos;
    }
}