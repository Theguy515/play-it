using UnityEngine;

public class CarM : MonoBehaviour
{
    public float speed = 5f;
    public float destroyX = 15f;

    void Update()
    {
        // Move to the right
        transform.Translate(Vector3.right * speed * Time.deltaTime);

        // Delete when off screen
        if (transform.position.x > destroyX)
        {
            Destroy(gameObject);
        }
    }
}
