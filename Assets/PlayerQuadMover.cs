using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerQuadMover : MonoBehaviour
{

    [SerializeField] float speed = 8f;
    [SerializeField] Vector2 halfSize = new Vector2(2f, 2f);

    Rigidbody2D rb;

    void Awake() => rb = GetComponent<Rigidbody2D>();

    void Update()
    {
        if (Keyboard.current == null) return;

        float x = 0f, y = 0f;
        if (Keyboard.current.aKey.isPressed) x -= 1f;
        if (Keyboard.current.dKey.isPressed) x += 1f;
        if (Keyboard.current.sKey.isPressed) y -= 1f;
        if (Keyboard.current.wKey.isPressed) y += 1f;

        var dir = new Vector2(x, y).normalized;
        rb.linearVelocity = dir * speed;

        var p = transform.position;
        p.x = Mathf.Clamp(p.x, -halfSize.x, halfSize.x);
        p.y = Mathf.Clamp(p.y, -halfSize.y, halfSize.y);
        transform.position = p;
    }
}
