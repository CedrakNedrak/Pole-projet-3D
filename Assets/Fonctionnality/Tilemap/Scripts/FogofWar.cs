using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class FogOfWar : MonoBehaviour
{
    [SerializeField] private Tilemap darkFogMap;
    [SerializeField] private Tilemap softFogMap;

    [SerializeField] private TileBase darkFogTile;
    [SerializeField] private TileBase softFogTile;

    [SerializeField] private Vector2 baseNormalizedPos = new Vector2(0.5f, 0.5f);

    [SerializeField] private int revealRadius = 2;
    [SerializeField] private bool useCircularReveal = true;

    private BoundsInt bounds;
    private Vector3Int baseCell;

    public void InitializeFog()
    {
        int width = TileGenerator.tileGenerator.Width;
        int height = TileGenerator.tileGenerator.Length;

        bounds = new BoundsInt(0, 0, 0, width, height, 1);

        baseCell = new Vector3Int(
            Mathf.RoundToInt((width - 1) * baseNormalizedPos.x),
            Mathf.RoundToInt((height - 1) * baseNormalizedPos.y),
            0
        );

        darkFogMap.ClearAllTiles();
        softFogMap.ClearAllTiles();

        foreach (Vector3Int pos in bounds.allPositionsWithin)
        {
            darkFogMap.SetTile(pos, darkFogTile);
            softFogMap.SetTile(pos, null);
        }

        RefreshVisibility();
    }

    public void RefreshVisibility()
    {
        HashSet<Vector3Int> visibleCells = GetReachableCellsFromBase();
        HashSet<Vector3Int> softVisibleCells = ExpandVisibleArea(visibleCells);

        foreach (Vector3Int pos in bounds.allPositionsWithin)
        {
            if (visibleCells.Contains(pos))
            {
                darkFogMap.SetTile(pos, null);
                softFogMap.SetTile(pos, null);
            }
            else if (softVisibleCells.Contains(pos))
            {
                darkFogMap.SetTile(pos, null);
                softFogMap.SetTile(pos, softFogTile);
            }
            else
            {
                darkFogMap.SetTile(pos, darkFogTile);
                softFogMap.SetTile(pos, null);
            }
        }
    }

    private HashSet<Vector3Int> GetReachableCellsFromBase()
    {
        HashSet<Vector3Int> visited = new HashSet<Vector3Int>();
        Queue<Vector3Int> queue = new Queue<Vector3Int>();

        if (!IsWalkable(baseCell))
            return visited;

        queue.Enqueue(baseCell);
        visited.Add(baseCell);

        Vector3Int[] directions =
        {
            new Vector3Int(1, 0, 0),
            new Vector3Int(-1, 0, 0),
            new Vector3Int(0, 1, 0),
            new Vector3Int(0, -1, 0)
        };

        while (queue.Count > 0)
        {
            Vector3Int current = queue.Dequeue();

            foreach (Vector3Int dir in directions)
            {
                Vector3Int next = current + dir;

                if (!bounds.Contains(next))
                    continue;

                if (visited.Contains(next))
                    continue;

                if (!IsWalkable(next))
                    continue;

                visited.Add(next);
                queue.Enqueue(next);
            }
        }

        return visited;
    }

    private HashSet<Vector3Int> ExpandVisibleArea(HashSet<Vector3Int> sourceCells)
    {
        HashSet<Vector3Int> expanded = new HashSet<Vector3Int>();

        foreach (Vector3Int cell in sourceCells)
        {
            for (int dx = -revealRadius; dx <= revealRadius; dx++)
            {
                for (int dy = -revealRadius; dy <= revealRadius; dy++)
                {
                    Vector3Int nearby = new Vector3Int(cell.x + dx, cell.y + dy, 0);

                    if (!bounds.Contains(nearby))
                        continue;

                    if (useCircularReveal)
                    {
                        if (dx * dx + dy * dy <= revealRadius * revealRadius)
                            expanded.Add(nearby);
                    }
                    else
                    {
                        expanded.Add(nearby);
                    }
                }
            }
        }

        return expanded;
    }

    private bool IsWalkable(Vector3Int cell)
    {
        return TileGenerator.tileGenerator.IsWalkable(cell.x, cell.y);
    }
}