using UnityEngine;

public class TurretAim : MonoBehaviour
{
    private Camera mainCam;

    private void Awake()
    {
        mainCam = Camera.main;
    }

    private void Update()
    {
        if(mainCam == null) return;

        Vector3 mouseScreenPos = Input.mousePosition;

        Vector3 mouseWorldPos = mainCam.ScreenToWorldPoint(mouseScreenPos);

        mouseWorldPos.z = transform.position.z;

        Vector2 dir = mouseWorldPos - transform.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

        angle -= 90f;

        transform.rotation = Quaternion.Euler(0f, 0f, angle);
    }
}
