using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class RuinsPlacer : MonoBehaviour
{
    [Header("Références")]
    [SerializeField] private CaverneGeneration caveGen;
    [SerializeField] private GameObject ruinPrefab;

    [Header("Probabilité par salle (hors base)")]
    [Range(0f, 1f)]
    [SerializeField] private float ruinRoomChance = 0.4f;

    public void PlaceRuins()
    {
        if (caveGen == null || ruinPrefab == null)
        {
            Debug.LogError("[RuinsPlacer] Références manquantes !");
            return;
        }

        Tilemap map = caveGen.Tilemap;
        if (map == null)
        {
            Debug.LogError("[RuinsPlacer] Tilemap logique introuvable !");
            return;
        }

        List<Vector2Int> centers = caveGen.GetNonBaseRoomCenters();
        BoundsInt bounds = caveGen.GetBounds();

        int placed = 0;

        foreach (Vector2Int center in centers)
        {
            if (Random.value > ruinRoomChance)
                continue;

            Vector3Int cell = new Vector3Int(
                bounds.xMin + center.x,
                bounds.yMin + center.y,
                0
            );

            // On n'écrit rien dans la tilemap logique
            Vector3 worldPos = map.GetCellCenterWorld(cell);
            Instantiate(ruinPrefab, worldPos, Quaternion.identity);
            placed++;
        }

        Debug.Log($"[RuinsPlacer] Ruines placées : {placed}/{centers.Count}");
    }
}