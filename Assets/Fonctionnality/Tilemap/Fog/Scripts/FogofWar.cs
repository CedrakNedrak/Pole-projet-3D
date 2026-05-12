using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class FogOfWar : MonoBehaviour
{
    [SerializeField] private DiscoveryManager discoveryManager;

    [SerializeField] private Tilemap darkFogMap;
    [SerializeField] private Tilemap softFogMap;

    [SerializeField] private TileBase darkFogTile;
    [SerializeField] private TileBase softFogTile;

    [SerializeField] private Vector2 baseNormalizedPos = new Vector2(0.5f, 0.5f);

    [SerializeField] private int revealRadius = 2;
    [SerializeField] private bool useCircularReveal = true;

    private BoundsInt bounds;
    private Vector3Int baseCell;

    private HashSet<Vector3Int> visibleCells = new HashSet<Vector3Int>();
    private HashSet<Vector3Int> softVisibleCells = new HashSet<Vector3Int>();

    private Vector3Int[] directions =
    {
        new Vector3Int(1, 0, 0),
        new Vector3Int(-1, 0, 0),
        new Vector3Int(0, 1, 0),
        new Vector3Int(0, -1, 0)
    };

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

        visibleCells.Clear();
        softVisibleCells.Clear();

        darkFogMap.ClearAllTiles();
        softFogMap.ClearAllTiles();

        foreach (Vector3Int pos in bounds.allPositionsWithin)
        {
            darkFogMap.SetTile(pos, darkFogTile);
            softFogMap.SetTile(pos, null);
        }

        RefreshVisibility(baseCell);
    }

    public void RefreshVisibility(Vector3Int dugCell)
    {
        if (!bounds.Contains(dugCell))
            return;

        if (!IsWalkable(dugCell))
            return;

        bool connectedToVisible = visibleCells.Contains(dugCell);

        // IMPORTANT : au début, la base doit pouvoir lancer la découverte
        if (visibleCells.Count == 0 && dugCell == baseCell)
            connectedToVisible = true;

        if (!connectedToVisible)
        {
            foreach (Vector3Int dir in directions)
            {
                Vector3Int neighbor = dugCell + dir;

                if (visibleCells.Contains(neighbor))
                {
                    connectedToVisible = true;
                    break;
                }
            }
        }

        if (!connectedToVisible)
            return;

        Queue<Vector3Int> queue = new Queue<Vector3Int>();

        visibleCells.Add(dugCell);
        queue.Enqueue(dugCell);

        SetVisible(dugCell);
        UpdateSoftFogAround(dugCell);

        while (queue.Count > 0)
        {
            Vector3Int current = queue.Dequeue();

            foreach (Vector3Int dir in directions)
            {
                Vector3Int next = current + dir;

                if (!bounds.Contains(next))
                    continue;

                if (visibleCells.Contains(next))
                    continue;

                if (!IsWalkable(next))
                    continue;

                visibleCells.Add(next);
                queue.Enqueue(next);

                SetVisible(next);
                UpdateSoftFogAround(next);
            }
        }
    }

    private void SetVisible(Vector3Int cell)
    {
        darkFogMap.SetTile(cell, null);
        softFogMap.SetTile(cell, null);

        softVisibleCells.Remove(cell);

        if (discoveryManager != null)
            discoveryManager.RevealAt(cell);
    }

    private void UpdateSoftFogAround(Vector3Int center)
    {
        for (int dx = -revealRadius; dx <= revealRadius; dx++)
        {
            for (int dy = -revealRadius; dy <= revealRadius; dy++)
            {
                Vector3Int nearby = new Vector3Int(center.x + dx, center.y + dy, 0);

                if (!bounds.Contains(nearby))
                    continue;

                if (visibleCells.Contains(nearby))
                    continue;

                if (useCircularReveal && dx * dx + dy * dy > revealRadius * revealRadius)
                    continue;

                if (!softVisibleCells.Contains(nearby))
                {
                    softVisibleCells.Add(nearby);

                    darkFogMap.SetTile(nearby, null);
                    softFogMap.SetTile(nearby, softFogTile);
                }
            }
        }
    }

    private bool IsWalkable(Vector3Int cell)
    {
        return TileGenerator.tileGenerator.IsWalkable(cell.x, cell.y);
    }
}