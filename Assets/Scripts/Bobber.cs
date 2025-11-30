using UnityEngine;

public class Bobber : MonoBehaviour
{
    Vector2 startPos;
    Vector2 targetPos;
    float travelTime;
    float elapsed;
    bool isTravelling = false;

    FisherCasting owner;

    public void Init(Vector2 direction, float distance, float travelTime, FisherCasting owner)
    {
        this.owner = owner;
        this.travelTime = travelTime;

        startPos = transform.position;
        targetPos = startPos + direction.normalized * distance;

        elapsed = 0f;
        isTravelling = true;
    }

    void Update()
    {
        if (!isTravelling) return;

        elapsed += Time.deltaTime;
        float t = Mathf.Clamp01(elapsed / travelTime);


        transform.position = Vector2.Lerp(startPos, targetPos, t);

        if (t >= 1f)
        {

            isTravelling = false;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {

        FishSpot spot = other.GetComponent<FishSpot>();
        if (spot != null)
        {

            FisherGameController.Instance.OnFishCaught(spot.points);

            spot.Despawn();

            Despawn();
        }
    }

    public void Despawn()
    {
        if (owner != null)
            owner.NotifyBobberGone();

        Destroy(gameObject);
    }
}