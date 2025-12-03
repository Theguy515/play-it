using UnityEngine;

public class FreeFallSpawner : MonoBehaviour
{
    [SerializeField] private FreeFallObstacleRow obstacleRowPrefab;

    [SerializeField] private float spawnInterval = 1.5f;

    // Where vertically to spawn (use the spawner's Y by default)
    [SerializeField] private float spawnY = -6f;

    // How far left/right rows are allowed to spawn
    [SerializeField] private float minX = -2.5f;
    [SerializeField] private float maxX =  2.5f;

    private float timer;

    private void Update()
    {
        timer -= Time.deltaTime;

        if (timer <= 0f)
        {
            SpawnRow();
            timer = spawnInterval;
        }
    }

    private void SpawnRow()
    {
        // Pick a random X between minX and maxX
        float x = Random.Range(minX, maxX);

        // Use that X, and the configured spawnY
        Vector3 spawnPos = new Vector3(x, spawnY, 0f);

        Instantiate(obstacleRowPrefab, spawnPos, Quaternion.identity);
    }
}