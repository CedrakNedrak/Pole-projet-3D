using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class CaverneGeneration : MonoBehaviour
{
    private Tilemap tilemap;
    public Tilemap Tilemap => tilemap;

    private BoundsInt bounds;

    public int Width { get; private set; }
    public int Height { get; private set; }

    [Header("Salles")]
    [SerializeField] private int roomCount = 8;
    [SerializeField] private int roomRadiusMin = 5;
    [SerializeField] private int roomRadiusMax = 10;

    [Header("Base")]
    [SerializeField] private int baseRadius = 10;
    [SerializeField] private Vector2 baseNormalizedPos = new Vector2(0.5f, 0.5f);

    [SerializeField] private int roomPadding = 3;

    public struct Room
    {
        public Vector2Int center;
        public int radius;
    }

    private List<Room> rooms = new List<Room>();
    public List<Vector2Int> UsedRoom { get; set; } = new();

    public void GenerateCaves(Tilemap map)
    {
        tilemap = map;
        ReadTilemapBounds();
        GenerateRoomsOnly();
    }

    public List<Vector2Int> GetNonBaseRoomCenters()
    {
        List<Vector2Int> centers = new List<Vector2Int>();

        for (int i = 1; i < rooms.Count; i++)
            centers.Add(rooms[i].center);

        return centers;
    }

    public BoundsInt GetBounds()
    {
        return bounds;
    }

    private void ReadTilemapBounds()
    {
        Width = TileGenerator.tileGenerator.Width;
        Height = TileGenerator.tileGenerator.Length;
        bounds = new BoundsInt(0, 0, 0, Width, Height, 1);
    }

    private void GenerateRoomsOnly()
    {
        rooms.Clear();
        UsedRoom.Clear();

        Vector2Int baseCenter = new Vector2Int(
            Mathf.RoundToInt((Width - 1) * baseNormalizedPos.x),
            Mathf.RoundToInt((Height - 1) * baseNormalizedPos.y)
        );

        Room baseRoom = new Room { center = baseCenter, radius = baseRadius };
        CarveCircle(baseRoom.center, baseRoom.radius);
        rooms.Add(baseRoom);
        UsedRoom.Add(baseCenter);

        int tries = 0;
        int created = 0;

        while (created < roomCount && tries < roomCount * 50)
        {
            tries++;

            int r = Random.Range(roomRadiusMin, roomRadiusMax + 1);
            int x = Random.Range(r + 2, Width - r - 2);
            int y = Random.Range(r + 2, Height - r - 2);

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
        foreach (Room r in rooms)
        {
            float dist = Vector2Int.Distance(candidate.center, r.center);

            if (dist < candidate.radius + r.radius + extraPadding)
                return true;
        }

        return false;
    }

    private void CarveCircle(Vector2Int center, int radius)
    {
        int r2 = radius * radius;

        for (int dx = -radius; dx <= radius; dx++)
        {
            for (int dy = -radius; dy <= radius; dy++)
            {
                if (dx * dx + dy * dy > r2 - radius)
                    continue;

                int gx = center.x + dx;
                int gy = center.y + dy;

                Vector3Int cell = new Vector3Int(bounds.xMin + gx, bounds.yMin + gy, 0);
                TileGenerator.tileGenerator.DigCell(cell);
            }
        }
    }
}