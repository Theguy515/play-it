using System.Collections.Generic;
using UnityEngine;

public class MazeGenerator : MonoBehaviour
{
    [Header("Maze Size (odd numbers work best)")]
    [SerializeField] int width = 15;
    [SerializeField] int height = 15;

    [Header("Prefabs")]
    [SerializeField] GameObject wallPrefab;
    [SerializeField] GameObject playerPrefab;
    [SerializeField] GameObject exitPrefab;

    [Header("Cell Settings")]
    [SerializeField] float cellSize = 1f;

    int[,] grid; // 1 = wall, 0 = floor
    Vector2Int startCell;
    Vector2Int exitCell;

    void Start()
    {
        // force odd dimensions so maze carves nicely
        if (width % 2 == 0) width++;
        if (height % 2 == 0) height++;

        GenerateMaze();
        BuildMaze();
    }

    void GenerateMaze()
    {
        grid = new int[width, height];

        // fill with walls
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                grid[x, y] = 1;
            }
        }

        startCell = new Vector2Int(1, 1);
        Carve(startCell.x, startCell.y);

        // simple exit: near opposite corner
        exitCell = new Vector2Int(width - 2, height - 2);
        grid[exitCell.x, exitCell.y] = 0;
    }

    void Carve(int x, int y)
    {
        grid[x, y] = 0;

        List<Vector2Int> dirs = new List<Vector2Int>
        {
            new Vector2Int(2, 0),
            new Vector2Int(-2, 0),
            new Vector2Int(0, 2),
            new Vector2Int(0, -2)
        };

        // shuffle directions
        for (int i = 0; i < dirs.Count; i++)
        {
            int rnd = Random.Range(i, dirs.Count);
            (dirs[i], dirs[rnd]) = (dirs[rnd], dirs[i]);
        }

        foreach (var d in dirs)
        {
            int nx = x + d.x;
            int ny = y + d.y;

            if (InBounds(nx, ny) && grid[nx, ny] == 1)
            {
                // knock down wall between
                int wx = x + d.x / 2;
                int wy = y + d.y / 2;
                grid[wx, wy] = 0;

                Carve(nx, ny);
            }
        }
    }

    bool InBounds(int x, int y)
    {
        return x > 0 && y > 0 && x < width - 1 && y < height - 1;
    }

    void BuildMaze()
    {
        // center maze around MazeGenerator position
        Vector2 offset = new Vector2(-width / 2f, -height / 2f) * cellSize;

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                Vector3 worldPos = new Vector3(x * cellSize, y * cellSize, 0) + (Vector3)offset + transform.position;

                if (grid[x, y] == 1)
                {
                    // wall
                    Instantiate(wallPrefab, worldPos, Quaternion.identity, transform);
                }
            }
        }

        // spawn player & exit
        Vector3 startPos = CellToWorld(startCell, offset);
        Vector3 exitPos = CellToWorld(exitCell, offset);

        GameObject player = Instantiate(playerPrefab, startPos, Quaternion.identity);
        Instantiate(exitPrefab, exitPos, Quaternion.identity);

        // Tell the camera to follow the player
        CameraFollow2D camFollow = Camera.main.GetComponent<CameraFollow2D>();
        if (camFollow != null)
        {
            camFollow.SetTarget(player.transform);
        }

    Vector3 CellToWorld(Vector2Int cell, Vector2 offset)
    {
        return new Vector3(cell.x * cellSize, cell.y * cellSize, 0) + (Vector3)offset + transform.position;
    }
}
}