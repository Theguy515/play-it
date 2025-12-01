using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    public GameObject[] obstaclePrefabs;   // set these in Inspector
    public float spawnRate = 1.5f;         // seconds between spawns

    private float timer = 0f;

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= spawnRate)
        {
            timer = 0f;
            SpawnRandomObstacle();
        }
    }

    void SpawnRandomObstacle()
    {
        if (obstaclePrefabs.Length == 0) return;

        int index = Random.Range(0, obstaclePrefabs.Length);
        Instantiate(obstaclePrefabs[index], transform.position, Quaternion.identity);
    }
}
