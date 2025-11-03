using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    public enum MovementType
    {
        RigidbodyVelocity,
        RigidbodyAddForce,
        VectorMoveToward,
        TransformTranslate,
        DirectPositionChange
    }

    [SerializeField]
    private float speed = 3f;

    [SerializeField] private Rigidbody2D body;

    private Vector2 axisMovement;

    [SerializeField]
    private MovementType movementType = MovementType.RigidbodyVelocity;
    
    private void Move()
    {
        switch (movementType)
        {
            case MovementType.RigidbodyVelocity:
                body.linearVelocity = axisMovement.normalized * speed;
                break;
            case MovementType.RigidbodyAddForce:
                // Use Force for smooth acceleration; avoids impulse tunneling
                body.AddForce(axisMovement.normalized * speed, ForceMode2D.Force);
                break;
            case MovementType.VectorMoveToward:
                // Use MovePosition instead of changing Transform to keep collisions stable
                body.MovePosition(body.position + axisMovement.normalized * speed * Time.fixedDeltaTime);
                break;
            case MovementType.TransformTranslate:
                // If you insist on transform movement, keep it here (will bypass physics)
                transform.Translate(axisMovement * speed * Time.deltaTime);
                break;
            case MovementType.DirectPositionChange:
                transform.position += (Vector3)axisMovement * Time.deltaTime * speed;
                break;
        }
    }
    private void Awake()
    {
        if(body == null)
        {
            body = GetComponent<Rigidbody2D>();
        }
    }
    void Update()
    {

        axisMovement = Vector2.zero;

        if(Keyboard.current != null)
        {

            var k = Keyboard.current;
            if (k.aKey.isPressed || k.leftArrowKey.isPressed)
            {
                axisMovement.x -= 1f;
            }
            if (k.dKey.isPressed || k.rightArrowKey.isPressed)
            {
                axisMovement.x += 1f;
            }
            if (k.wKey.isPressed || k.upArrowKey.isPressed)
            {
                axisMovement.y += 1f;
            }
            if (k.sKey.isPressed || k.downArrowKey.isPressed)
            {
                axisMovement.y -= 1f;
            }
            
        }
        axisMovement = Vector2.ClampMagnitude(axisMovement, 1f);
    }
    private void FixedUpdate()
    {
        Move();
    }
    private void RigidbodyVelocity()
    {
        body.linearVelocity = axisMovement.normalized * speed;
    }

    private void RigidbodyAddForce()
    {
        body.AddForce(axisMovement * speed, ForceMode2D.Force);
    }

    private void VectorMoveTowards()
    {
        transform.position = Vector2.MoveTowards(transform.position,
            transform.position + (Vector3)axisMovement, speed * Time.deltaTime);
    }

    private void TransformTranslate()
    {
        transform.Translate(axisMovement * speed * Time.deltaTime);
    }

    private void PositionChange()
    {
        transform.position += (Vector3)axisMovement * Time.deltaTime * speed;
    }
}
