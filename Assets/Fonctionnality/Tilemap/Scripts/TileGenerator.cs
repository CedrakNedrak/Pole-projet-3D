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
    [SerializeField] private TileBase crystalTile;

    [SerializeField] private int length;
    [SerializeField] private int width;

    [UnityEngine.Range(0f, 1f)]
    [SerializeField] private float crystalChance = 0.02f;

    public static TileGenerator tileGenerator;
    private GameObject[,] worldMatrice;
    private int[,] worldIntMatrice;
    public GameObject[,] WorldMatrice => worldMatrice;
    public int[,] WorldIntMatrice => worldIntMatrice;
    void Awake()
    {
        worldMatrice = new GameObject[width, length];
        worldIntMatrice = new int[width, length];
        tileGenerator = this;
    }

    void Start()
    {
        GenerateTilemap();
        caveGenerator.GenerateCaves(tilemap);
        //enemyCaverneGeneration.PlaceEnemyBase();
        ruinsPlacer.PlaceRuins();
        fogOfWar.InitializeFog();
    }

    void GenerateTilemap()
    {
        tilemap.ClearAllTiles();

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < length; y++)
            {
                Vector3Int position = new Vector3Int(x, y, 0);

                if (Random.value < crystalChance)
                {
                    GameObject gO = Instantiate(backgroundTile, position, Quaternion.Euler(90f, 0f, 0f), transform);
                    worldMatrice[x, y] = gO;
                    worldIntMatrice[x, y] = 2;
                }
                else
                {
                    GameObject gO = Instantiate(backgroundTile, position, Quaternion.Euler(90f, 0f, 0f), transform);
                    worldMatrice[x, y] = gO;
                    worldIntMatrice[x, y] = 2;
                }
            }
        }
    }

}
