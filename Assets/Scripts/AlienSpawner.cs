using UnityEngine;

public class AlienSpawner : MonoBehaviour
{
    [SerializeField] private AlienEnemy alienPrefab;
    [SerializeField] private Transform target;
    [SerializeField] private float spawnRadius = 8f;
    [SerializeField] private float spawnInterval = 1.5f;

    private float timer;
    
    private void Update()
    {
        if (!DefenseGameController.Instance.IsGameRunning) return;

        timer -= Time.deltaTime;
        if (timer <= 0f)
        {
            SpawnAlien();
            timer = spawnInterval;
        }
    }

    private void SpawnAlien()
    {
        float angle = Random.Range(0f, 360f) * Mathf.Deg2Rad;
        Vector3 offset = new Vector3(Mathf.Cos(angle), Mathf.Sin(angle), 0f) * spawnRadius;
        Vector3 spawnPos = target.position + offset;

        AlienEnemy alien = Instantiate(alienPrefab, spawnPos, Quaternion.identity);
        alien.Initialize(target);
    }
}
