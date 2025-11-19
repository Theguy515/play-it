using UnityEngine;

public class FourEdgeSpawner : MonoBehaviour
{
    [Header("Refs")]
    [SerializeField] GameObject hazardPrefab;

    [Header("Arena (centered at 0,0)")]
    [SerializeField] Vector2 halfSize = new Vector2(2f, 2f); // must match player

    [Header("Spawn")]
    [SerializeField] int slotsPerEdge = 4;
    [SerializeField] float spawnEvery = 0.75f;
    [SerializeField] float hazardSpeed = 6f;
    [SerializeField] float outsideOffset = 0.6f;


    [Header("Difficulty")]
    [SerializeField] float speedRampPerSec = 0.2f;
    [SerializeField] float spawnRampPerSec = 0.02f;
    [SerializeField] float minSpawnEvery = 0.25f;

    float t;

    void Update()
    {
        // ramp difficulty
        hazardSpeed += speedRampPerSec * Time.deltaTime;
        spawnEvery = Mathf.Max(minSpawnEvery, spawnEvery - spawnRampPerSec * Time.deltaTime);

        t += Time.deltaTime;
        if (t >= spawnEvery)
        {
            t = 0f;
            SpawnOne();
        }
    }

    void SpawnOne()
    {
        int edge = Random.Range(0, 4);
        int slot = Random.Range(0, slotsPerEdge);

        Vector2 pos, dir;

        switch (edge)
        {
            case 0: // Top
                pos = new Vector2(SlotPosition(-halfSize.x, halfSize.x, slot, slotsPerEdge),
                                  +halfSize.y + outsideOffset);
                dir = Vector2.down;
                break;
            case 1: // Bottom
                pos = new Vector2(SlotPosition(-halfSize.x, halfSize.x, slot, slotsPerEdge),
                                  -halfSize.y - outsideOffset);
                dir = Vector2.up;
                break;
            case 2: // Left
                pos = new Vector2(-halfSize.x - outsideOffset,
                                  SlotPosition(-halfSize.y, halfSize.y, slot, slotsPerEdge));
                dir = Vector2.right;
                break;
            default: // Right
                pos = new Vector2(+halfSize.x + outsideOffset,
                                  SlotPosition(-halfSize.y, halfSize.y, slot, slotsPerEdge));
                dir = Vector2.left;
                break;
        }

        var go = Instantiate(hazardPrefab, pos, Quaternion.identity);
        var hz = go.GetComponent<Hazard>();

        hz.Launch(dir * hazardSpeed);
    }

    static float SlotPosition(float min, float max, int slotIndex, int totalSlots)
    {
        // even distribution across the span (centered between tiles)
        float t = (slotIndex + 0.5f) / totalSlots;
        return Mathf.Lerp(min, max, t);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        var size = (halfSize * 2f) + new Vector2(outsideOffset * 2f, outsideOffset * 2f);
        Gizmos.DrawWireCube(Vector3.zero, new Vector3(size.x, size.y, 0f));
    }
}