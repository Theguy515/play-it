using UnityEngine;

public class FishSpot : MonoBehaviour
{
    public int points = 100;
    public float lifeTime = 3f;

    void Start()
    {
        Destroy(gameObject, lifeTime);
    }

    public void Despawn()
    {
        Destroy(gameObject);
    }
}