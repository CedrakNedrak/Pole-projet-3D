using UnityEngine;
using UnityEngine.Tilemaps;

public class TileGenerator : MonoBehaviour
{
    [SerializeField] private CaverneGeneration caveGenerator;
    [SerializeField] private RuinsPlacer ruinsPlacer;
    [SerializeField] private EnemyCaverneGeneration enemyCaverneGeneration;
    [SerializeField] private FogOfWar fogOfWar;

    [SerializeField] private Tilemap tilemap;
    public Tilemap Tilemap => tilemap;

    [SerializeField] private GameObject backgroundTile;

    [SerializeField] private int length = 200;
    [SerializeField] private int width = 200;

    public int Width => width;
    public int Length => length;

    public static TileGenerator tileGenerator;

    private GameObject[,] worldMatrice;
    private int[,] worldIntMatrice;

    public GameObject[,] WorldMatrice => worldMatrice;
    public int[,] WorldIntMatrice => worldIntMatrice;

    private bool fogInitialized = false;

    private void Awake()
    {
        worldMatrice = new GameObject[width, length];
        worldIntMatrice = new int[width, length];

        tileGenerator = this;
    }

    private void Start()
    {
        GenerateTilemap();

        caveGenerator.GenerateCaves(tilemap);

        if (enemyCaverneGeneration != null)
            enemyCaverneGeneration.PlaceEnemyBase();

        if (ruinsPlacer != null)
            ruinsPlacer.PlaceRuins();

        if (fogOfWar != null)
        {
            fogOfWar.InitializeFog();
            fogInitialized = true;
        }
    }

    private void GenerateTilemap()
    {
        tilemap.ClearAllTiles();

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < length; y++)
            {
                Vector3Int position = new Vector3Int(x, y, 0);

                GameObject gO = Instantiate(
                    backgroundTile,
                    position,
                    Quaternion.Euler(90f, 0f, 0f),
                    transform
                );

                worldMatrice[x, y] = gO;

                // 2 = mur / bloc plein
                worldIntMatrice[x, y] = 2;
            }
        }
    }

    public bool IsInBounds(int x, int y)
    {
        return x >= 0 && x < width && y >= 0 && y < length;
    }

    public bool IsWalkable(int x, int y)
    {
        if (!IsInBounds(x, y))
            return false;

        // 1 = vide / creusé / accessible
        return worldIntMatrice[x, y] == 1;
    }

    public void DigCell(Vector3Int cell)
    {
        int x = cell.x;
        int y = cell.y;

        if (!IsInBounds(x, y))
            return;

        if (worldMatrice[x, y] != null)
            worldMatrice[x, y].SetActive(false);

        worldIntMatrice[x, y] = 1;

        if (fogInitialized && fogOfWar != null)
            fogOfWar.RefreshVisibility();
    }
}
