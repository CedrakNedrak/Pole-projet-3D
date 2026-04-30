using NUnit.Framework.Constraints;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class EnemyCaverneGeneration : MonoBehaviour
{
    [Header("Références")]
    [SerializeField] private CaverneGeneration caveGen;
   
    private Tilemap map;
    [SerializeField] private TileBase ruinTile;
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

        map.SetTile(mainCell, ruinTile);
        caveGen.UsedRoom.Add(centers[valeur]);

        for (int i = 0; i < numOfEnnemyBase; i++)
        {
            float minMag = (float) caveGen.Height + caveGen.Width;

            Vector2Int enemyBase = new Vector2Int();
            foreach (var center in centers)
            {
                if (caveGen.UsedRoom.Contains(center))
                    continue;

                Debug.Log(minMag);
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
            List<Vector3Int> path = Pathfinding.pathfinding.Launch(enemyBase, centers[valeur]);
            foreach(var val in path)
            {
                GameObject go = TileGenerator.tileGenerator.WorldMatrice[val.x, val.y];
                TileGenerator.tileGenerator.WorldIntMatrice[val.x, val.y] = 1;
                go.SetActive(false);
            }

            map.SetTile(secondCell, ruinTile);
            caveGen.UsedRoom.Add(enemyBase);
        }
    }
}
