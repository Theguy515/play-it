using UnityEngine;

public class TurretShoot : MonoBehaviour
{

    [SerializeField] private Bullet bulletPrefab;
    [SerializeField] private Transform firePoint;
    [SerializeField] private float fireCooldown = 0.5f;

    private float lastFireTime;

    private void Update()
    {
        if(Input.GetMouseButton(0))
        {
            TryShoot();
        }
    }

    private void TryShoot()
    {
        if(Time.time < lastFireTime + fireCooldown) return;

        lastFireTime = Time.time;

        //From firepoint to mouse
        Vector3 mouse = Input.mousePosition;
        mouse.z = Mathf.Abs(Camera.main.transform.position.z);
        Vector3 mouseWorld = Camera.main.ScreenToWorldPoint(mouse);

        Vector2 dir = (mouseWorld - firePoint.position).normalized;

        Bullet b = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
        b.Initialize(dir);
    }
}
