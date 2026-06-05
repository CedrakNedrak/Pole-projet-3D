using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class EnemyCaverneGeneration : MonoBehaviour
{
    [Header("Références")]
    [SerializeField] private CaverneGeneration caveGen;

    private Tilemap map;
    [SerializeField] private GameObject enemySpawner;
    [SerializeField] private GameObject enemyBase;
    [SerializeField] private int numOfEnnemyBase = 3;

    public void PlaceEnemyBase()
    {
        map = caveGen.Tilemap;

        List<Vector2Int> centers = caveGen.GetNonBaseRoomCenters();
        BoundsInt bounds = caveGen.GetBounds();

        int valeur = Random.Range(1, centers.Count);

        Vector3Int mainCell = new Vector3Int(
            bounds.xMin + centers[valeur].x,
            bounds.yMin + centers[valeur].y,
            0
        );

        caveGen.UsedRoom.Add(centers[valeur]);

        Instantiate(enemyBase, new Vector3(centers[valeur].x, centers[valeur].y, 0),Quaternion.Euler(90f, 0f, 0f), transform);

        for (int i = 0; i < numOfEnnemyBase; i++)
        {
            float minMag = (float)caveGen.Height + caveGen.Width;

            Vector2Int enemyBase = new Vector2Int();
            foreach (var center in centers)
            {
                if (caveGen.UsedRoom.Contains(center))
                    continue;
                if ((center - new Vector2(mainCell.x, mainCell.y)).magnitude < minMag)
                {
                    minMag = (center - new Vector2(mainCell.x, mainCell.y)).magnitude;
                    enemyBase = center;
                }
            }

            Vector3Int secondCell = new Vector3Int(
                bounds.xMin + enemyBase.x,
                bounds.yMin + enemyBase.y,
                0
            );
            List<Vector3Int> path = Pathfinding.pathfinding.Launch(enemyBase, centers[valeur], TileGenerator.tileGenerator.MiningWorldIntMatrice);

            foreach (var val in path)
            {
                GameObject go = TileGenerator.tileGenerator.WorldMatrice[val.x, val.y];
                TileGenerator.tileGenerator.MiningWorldIntMatrice[val.x, val.y] = 1;
                TileGenerator.tileGenerator.NormalWorldIntMatrice[val.x, val.y] = 1;
                Debug.Log("Dig");
                go.SetActive(false);
            }

            Instantiate(enemySpawner, secondCell, Quaternion.Euler(90f, 0f, 0f), transform);
            caveGen.UsedRoom.Add(enemyBase);
        }
    }
}
