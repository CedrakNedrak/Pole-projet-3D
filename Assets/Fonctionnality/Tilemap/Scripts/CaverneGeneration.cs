using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UIElements;

public class CaverneGeneration : MonoBehaviour
{
    private Tilemap tilemap;
    public Tilemap Tilemap => tilemap;

    private BoundsInt bounds;
    private int width, height;

    [Header("Salles (cavernes)")]
    [SerializeField] int roomCount = 8;
    [SerializeField] int roomRadiusMin = 5;
    [SerializeField] int roomRadiusMax = 10;

    [Header("Base / Taverne")]
    [SerializeField] int baseRadius = 10;   // taille de la taverne de départ
    [SerializeField] Vector2 baseNormalizedPos = new Vector2(0.5f, 0.5f); // pos de la taverne de départ

    //pour éviter que les salles se chevauchent trop
    [SerializeField] int roomPadding = 3;

    private struct Room
    {
        public Vector2Int center;
        public int radius;
    }

    private List<Room> rooms = new List<Room>();

    public void GenerateCaves(Tilemap map)
    {
        tilemap = map;
        ReadTilemapBounds();
        GenerateRoomsOnly();
    }
    // ------------- Lire les infos des salles (position, taille) -------------

    public List<Vector2Int> GetNonBaseRoomCenters()
    {
        List<Vector2Int> centers = new List<Vector2Int>();

        for (int i = 1; i < rooms.Count; i++) // 0 = base
        {
            centers.Add(rooms[i].center);
        }

        return centers;
    }
    public BoundsInt GetBounds()
    {
        return bounds;
    }
    // ------------- Lire la zone (bounds) -------------
    private void ReadTilemapBounds()
    {
        width = 200;
        height = 200;
    }

    // ------------- Générer UNIQUEMENT des salles (non connectées) -------------
    private void GenerateRoomsOnly()
    {
        rooms.Clear();

        // A) Base/taverne garantie
        Vector2Int baseCenter = new Vector2Int(
            Mathf.RoundToInt((width - 1) * baseNormalizedPos.x),
            Mathf.RoundToInt((height - 1) * baseNormalizedPos.y)
        );

        Room baseRoom = new Room { center = baseCenter, radius = baseRadius };
        CarveCircle(baseRoom.center, baseRoom.radius);
        rooms.Add(baseRoom);

        // B) Autres salles circulaires
        int tries = 0;
        int created = 0;

        while (created < roomCount && tries < roomCount * 50)
        {
            tries++;

            int r = Random.Range(roomRadiusMin, roomRadiusMax + 1);

            // éviter les bords pour que le cercle tienne dans la map
            int x = Random.Range(r + 2, width - r - 2);
            int y = Random.Range(r + 2, height - r - 2);

            Room newRoom = new Room { center = new Vector2Int(x, y), radius = r };

            if (IsTooCloseToExistingRooms(newRoom, roomPadding))
                continue;

            CarveCircle(newRoom.center, newRoom.radius);
            rooms.Add(newRoom);
            created++;
        }
    }

    private bool IsTooCloseToExistingRooms(Room candidate, int extraPadding)
    {
        foreach (var r in rooms)
        {
            float dist = Vector2Int.Distance(candidate.center, r.center);
            if (dist < (candidate.radius + r.radius + extraPadding))
                return true;
        }
        return false;
    }

    private bool InBounds(int x, int y)
        => x >= 0 && y >= 0 && x < width && y < height;

    // Creuse une salle (cercle approx sur grille)
    private void CarveCircle(Vector2Int center, int radius)
    {
        int r2 = radius * radius;

        for (int dx = -radius; dx <= radius; dx++)
        {
            for (int dy = -radius; dy <= radius; dy++)
            {
                // Si on est EN DEHORS du cercle -> on saute
                if (dx * dx + dy * dy > r2 - radius) continue;

                int gx = center.x + dx;
                int gy = center.y + dy;


                Vector3Int cell = new Vector3Int(bounds.xMin + gx, bounds.yMin + gy, 0);
                if (cell.x < 200 && cell.x >= 0 && 0 <= cell.y && cell.y < 200)
                {
                    GameObject go = TileGenerator.tileGenerator.WorldMatrice[cell.x, cell.y];
                    go.SetActive(false);
                }

            }
        }
    }

    

}
