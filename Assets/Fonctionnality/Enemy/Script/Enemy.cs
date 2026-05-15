using System;
using System.Collections.Generic;
using System.IO;
using UnityEditorInternal;
using UnityEngine;
using Random = UnityEngine.Random;

public class Enemy : CharaMovement
{
    public CaverneGeneration Cavegen { get; set; }
    private void Start()
    {
        Vector3Int position = new Vector3Int((int)transform.position.x, (int)transform.position.y, 0);
        int x = Random.Range(-1, 2);
        int y = Random.Range(-1, 2);
        Vector3Int nextPos = position + new Vector3Int(x, y, 0);
        while (x * x == y * y || TileGenerator.tileGenerator.WorldIntMatrice[nextPos.x, nextPos.y] == 2)
        {
            x = Random.Range(-1, 2);
            y = Random.Range(-1, 2);

            nextPos = position + new Vector3Int(x, y, 0);
        }

        StartTween(nextPos);
    }

    protected override void ChangeTween(){

        Vector3Int position = new Vector3Int((int)transform.position.x, (int)transform.position.y, 0);
        int x = Random.Range(-1, 2);
        int y = Random.Range(-1, 2);
        Vector3Int nextPos = position + new Vector3Int(x, y, 0);
        while (x*x==y*y || TileGenerator.tileGenerator.WorldIntMatrice[nextPos.x,nextPos.y] == 2)
        {
            x = Random.Range(-1, 2);
            y = Random.Range(-1, 2);

            nextPos = position + new Vector3Int(x, y, 0);
        }
            
        StartTween(nextPos);
    }
}
