using UnityEngine;

public class CameraF: MonoBehaviour
{
    public Transform target;   // player
    public float smoothSpeed = 5f; 
    public Vector3 offset;     

    void LateUpdate()
    {
        if (target == null) return;

        Vector3 desiredPos = target.position + offset;
        Vector3 smoothedPos = Vector3.Lerp(transform.position, desiredPos, smoothSpeed * Time.deltaTime);

        // Keep camera in 2D by locking Z
        smoothedPos.z = -10f;  

        transform.position = smoothedPos;
    }
}
