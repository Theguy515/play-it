using UnityEngine;

public class Bullet : MonoBehaviour
{

    [SerializeField] private float speed = 10f;
    [SerializeField] private float lifetime = 3f;

    private Vector2 direction;

    public void Initialize(Vector2 dir)
    {
        direction = dir.normalized;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, angle - 90f);
    }

    private void Start()
    {
        Destroy(gameObject, lifetime);
    }
    private void Update()
    {
        transform.position += (Vector3)direction * speed * Time.deltaTime;
    }
}
