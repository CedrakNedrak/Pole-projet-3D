using UnityEngine;
using UnityEngine.Tilemaps;
using System.Collections.Generic;

public class RuinsPlacer : MonoBehaviour
{
    [Header("Références")]
    [SerializeField] private CaverneGeneration caveGen;
    private Tilemap map;
    [SerializeField] private TileBase ruinTile;

    [Header("Probabilité par salle (hors base)")]
    [Range(0f, 1f)]
    [SerializeField] private float ruinRoomChance = 0.4f;

   

    public void PlaceRuins()
    {
        map = caveGen.Tilemap;
        if (caveGen == null || map == null || ruinTile == null)
        {
            Debug.LogError("[RuinsPlacer] Références manquantes !");
            return;
        }

        List<Vector2Int> centers = caveGen.GetNonBaseRoomCenters();
        BoundsInt bounds = caveGen.GetBounds();

        int placed = 0;

        foreach (var center in centers)
        {
            if (Random.value > ruinRoomChance)
                continue;
            if (caveGen.UsedRoom.Contains(center))
                continue;

            Vector3Int cell = new Vector3Int(
                bounds.xMin + center.x,
                bounds.yMin + center.y,
                0
            );
            Debug.Log($"Place ruin at cell {cell} | current tile = {map.GetTile(cell)}");
            caveGen.UsedRoom.Add(new Vector2Int( cell.x, cell.y ));
            map.SetTile(cell, ruinTile);
            placed++;
        }

        Debug.Log($"[RuinsPlacer] Ruines placées : {placed}/{centers.Count}");
    }
}
