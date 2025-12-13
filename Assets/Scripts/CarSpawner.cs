using UnityEngine;

public class CarSpawner : MonoBehaviour
{
    public GameObject carPrefab;
    public Transform[] spawnPoints;
    public float minSpawnTime = 1f;
    public float maxSpawnTime = 3f;

    private float timer;

    void Start()
    {
        ResetTimer();
    }

    void Update()
    {
        timer -= Time.deltaTime;

        if (timer <= 0f)
        {
            SpawnCar();
            ResetTimer();
        }
    }

    void SpawnCar()
    {
        if (carPrefab == null || spawnPoints.Length == 0) return;

        int index = Random.Range(0, spawnPoints.Length);
        Transform point = spawnPoints[index];

        Instantiate(carPrefab, point.position, point.rotation);
    }

    void ResetTimer()
    {
        timer = Random.Range(minSpawnTime, maxSpawnTime);
    }
}
