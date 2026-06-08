using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class RuinsPlacer : MonoBehaviour
{
    [Header("Références")]
    [SerializeField] private CaverneGeneration caveGen;
    private Tilemap map;
    [SerializeField] private GameplayManager gameplayManager;

    [Range(0f, 1f)]
    [SerializeField] private float ruinRoomChance = 0.6f;

    [Header("Noms des villes")]
    private List<string> townNames = new List<string>
    {
    // Ancient & Mysterious
    "Gloomhollow",
    "Umbravale",
    "Deepreach",
    "Echoforge",
    "Stonewhisper",
    "Gravemarrow",
    "Old Chasm",
    "The Sunken Vault",
    "Hollowspire",
    "Blackroot Sanctum",

    // Volcanic / Magma
    "Emberdeep",
    "Ashenhold",
    "Moltrune",
    "Pyrecradle",
    "Cinderfall",
    "Lavaheart Enclave",
    "Smoldergate",
    "Redrift Caverns",

    // Crystal / Mineral
    "Shardhaven",
    "Gleamspire",
    "Quartzreach",
    "Lumengrotto",
    "Opaline Ward",
    "Crystalbarrow",
    "Prismhall",
    "Gemspire Refuge",

    // Fungal / Bioluminescent
    "Mireglow",
    "Sporevale",
    "Fungalrest",
    "Glowmire",
    "Mossreach",
    "Mycoria",
    "Shimmerfen",
    "Bluecap Hollow",

    // Dwarven / Industrial
    "Ironburrow",
    "Hammerdeep",
    "Forgehold",
    "Stonecrank",
    "Gearhollow",
    "Pickaxe Gate",
    "Orehearth",
    "Anvilroot",

    // Creepy / Abandoned
    "Whisperchasm",
    "Forsaken Delve",
    "The Lost Hollows",
    "Grimshaft",
    "Deadlight Cavern",
    "Rotspire",
    "Bleakmarrow",
    "The Forgotten Pit",

    // Magical / Arcane
    "Aethergloom",
    "Runeburrow",
    "Spellrift",
    "Arcane Hollow",
    "Eldershade",
    "Mystvale Below",
    "Starless Sanctum",
    "The Under-Archive"
    };

    public void PlaceRuins()
    {
        if (caveGen == null)
        {
            Debug.LogError("caveGen is missing!");
            return;
        }

        Tilemap map = caveGen.Tilemap;

        if (map == null)
        {
            Debug.LogError("[RuinsPlacer] Tilemap introuvable !");
            return;
        }

        List<Vector2Int> centers = caveGen.GetNonBaseRoomCenters();
        BoundsInt bounds = caveGen.GetBounds();

        int placed = 0;

        foreach (Vector2Int center in centers)
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
            caveGen.UsedRoom.Add(new Vector2Int(cell.x, cell.y));
            string townName = townNames[Random.Range(0, townNames.Count)];
            gameplayManager.SpawnTown(cell.ToWorldScale(), Quaternion.Euler(-90,0,0), townName, 0, 40, 0, 200, 40, 1000);
            caveGen.UsedRoom.Add(center);
            placed++;
        }

        Debug.Log($"[RuinsPlacer] Ruines placées : {placed}/{centers.Count}");
    }
}