using UnityEngine;

public class CrossMovment : MonoBehaviour
{
    public float stepSize = 1f;   // how far you move each press
    public float moveSpeed = 10f; // how fast you slide into the new spot

    private Vector3 targetPosition;
    private bool isMoving = false;

    void Start()
    {
        targetPosition = transform.position;

        // For 2D, keep Z locked so you don't drift "under" things.
        targetPosition.z = 0f;
        transform.position = targetPosition;
    }

    void Update()
    {
        if (!isMoving)
        {
            Vector3 input = Vector3.zero;

            // ✅ 2D top-down movement: use Y for up/down, X for left/right
            if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
                input = Vector3.up;
            else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
                input = Vector3.down;
            else if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
                input = Vector3.left;
            else if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
                input = Vector3.right;

            if (input != Vector3.zero)
            {
                targetPosition = transform.position + input * stepSize;

                // ✅ lock Z so it never changes
                targetPosition.z = 0f;

                isMoving = true;
            }
        }
        else
        {
            transform.position = Vector3.MoveTowards(
                transform.position,
                targetPosition,
                moveSpeed * Time.deltaTime
            );

            // ✅ Use sqrMagnitude (faster + avoids tiny float issues)
            if ((transform.position - targetPosition).sqrMagnitude < 0.000001f)
            {
                transform.position = targetPosition;
                isMoving = false;
            }
        }
    }
}
