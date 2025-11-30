using UnityEngine;

public class FishSpawner : MonoBehaviour
{
    [Header("Spawn Settings")]
    [SerializeField] GameObject fishSpotPrefab;
    [SerializeField] float spawnInterval = 2f;
    [SerializeField] int maxFishSpots = 5;


    [SerializeField] Vector2 areaMin; // bottom-left corner
    [SerializeField] Vector2 areaMax; // top-right corner

    float timer;

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= spawnInterval)
        {
            timer = 0f;
            TrySpawnFish();
        }
    }

    void TrySpawnFish()
    {
        if (fishSpotPrefab == null) return;


        int current = GameObject.FindGameObjectsWithTag("FishSpot").Length;
        if (current >= maxFishSpots) return;

        float x = Random.Range(areaMin.x, areaMax.x);
        float y = Random.Range(areaMin.y, areaMax.y);
        Vector3 spawnPos = new Vector3(x, y, 0f);

        Instantiate(fishSpotPrefab, spawnPos, Quaternion.identity);
    }
}