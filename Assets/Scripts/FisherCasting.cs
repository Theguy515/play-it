using UnityEngine;
using UnityEngine.UI;

public class FisherCasting : MonoBehaviour
{
    [Header("References")]
    [SerializeField] Transform castPoint;      // where the bobber spawns
    [SerializeField] GameObject bobberPrefab;  // bobber with Rigidbody2D
    [SerializeField] Image powerBarFill;
    [SerializeField] LineRenderer line;

    [Header("Throw Settings")]
    [SerializeField] float minPower = 2f;
    [SerializeField] float maxPower = 8f;
    [SerializeField] float maxHoldTime = 1.5f;   // time to reach full power
    [SerializeField] float bobberTravelTime = 0.5f;
    bool charging;
    float holdTime;
    Vector2 lastAimDirection = Vector2.right;
    GameObject currentBobber;

    void Update()
    {
        AimAtMouse();
        HandleInput();

        if (currentBobber != null)
        {
            line.SetPosition(0, castPoint.position);
            line.SetPosition(1, currentBobber.transform.position);
        }

        if (currentBobber == null && line.enabled)
        {
            line.enabled = false;
        }
    }

    void AimAtMouse()
    {
        // Mouse position in world
        Vector3 mouseWorld = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouseWorld.z = 0f;

        // Direction from cast point to mouse
        Vector2 dir = (mouseWorld - castPoint.position).normalized;

        if (dir.sqrMagnitude < 0.0001f)
            dir = Vector2.right;

        lastAimDirection = dir;
    }

    void HandleInput()
{

    if (currentBobber != null)
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ClearBobber();
        }
        return; 
    }

    // Start charging when Space is pressed
    if (Input.GetKeyDown(KeyCode.Space))
    {
        charging = true;
        holdTime = 0f;
    }


    if (charging && Input.GetKey(KeyCode.Space))
    {
        holdTime += Time.deltaTime;
        float chargePercent = Mathf.Clamp01(holdTime / maxHoldTime);

        if (powerBarFill != null)
            powerBarFill.fillAmount = chargePercent;
    }


    if (charging && Input.GetKeyUp(KeyCode.Space))
    {
        charging = false;

        float chargePercent = Mathf.Clamp01(holdTime / maxHoldTime);
        float distance = Mathf.Lerp(minPower, maxPower, chargePercent);

        CastBobber(distance);

        holdTime = 0f;
        if (powerBarFill != null)
            powerBarFill.fillAmount = 0f;
    }
}

    void CastBobber(float power)
{
    if (bobberPrefab == null || castPoint == null)
        return;

    GameObject bobberObj = Instantiate(
        bobberPrefab,
        castPoint.position,
        Quaternion.identity
    );

    currentBobber = bobberObj;

    Bobber bobber = bobberObj.GetComponent<Bobber>();
    if (bobber != null)
    {

        bobber.Init(lastAimDirection, power, bobberTravelTime, this);
    }

    line.enabled = true;
}

public void NotifyBobberGone()
{
    currentBobber = null;
    line.enabled = false;
}
    void ClearBobber()
    {
        if (currentBobber != null)
        {
            Destroy(currentBobber);
            currentBobber = null;
        }

        if (line != null)
            line.enabled = false;
    }
}