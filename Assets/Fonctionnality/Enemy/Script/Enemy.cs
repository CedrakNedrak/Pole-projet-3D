using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using Random = UnityEngine.Random;

public class Enemy : CharaMovement
{
    public CaverneGeneration Cavegen { get; set; }
    private void Start()
    {
        int x = Random.Range(0, 200);
        int y = Random.Range(0, 200);

        path = Pathfinding.pathfinding.Launch(new Vector2Int((int)transform.position.x, (int)transform.position.y), new Vector2Int(x, y));
        StartTween(path[0]);
    }

    void Update()
    {
        int x = path[tweenEnCours].x;
        int y = path[tweenEnCours].y;
        if (TileGenerator.tileGenerator.WorldIntMatrice[x,y] == 2)
        {
            StopTween();
            tweenEnCours = 0;
            x = Random.Range(0, 200);
            y = Random.Range(0, 200);

            path = Pathfinding.pathfinding.Launch(new Vector2Int((int)transform.position.x, (int)transform.position.y), new Vector2Int(x, y));
            StartTween(path[0]);
        }
    }
}
