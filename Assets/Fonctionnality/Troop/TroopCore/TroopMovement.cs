using System;
using System.Collections.Generic;
using UnityEngine;

public class TroopMovement : CharaMovement
{
    public enum TroopType
    {
        NormalTroop,
        MiningTroop
    }
    protected TroopType troopType;

    protected Vector3 endPosition;
    protected Vector3 startPosition = new Vector3(0, 2, 0);
    public static Dictionary<TroopMovement.TroopType, int[,]> TroopTypeToGrid { set; get; }
    protected virtual void Start()
    {
        TroopTypeToGrid = new Dictionary<TroopMovement.TroopType, int[,]>
        {
            { TroopMovement.TroopType.MiningTroop, TileGenerator.tileGenerator.MiningWorldIntMatrice },
            { TroopMovement.TroopType.NormalTroop, TileGenerator.tileGenerator.NormalWorldIntMatrice },
        };
    }

    public TroopMovement(TroopType troopType)
    {
        this.troopType = troopType;
    }

    public void StartMoving(Vector3 startPos, Vector3 endPos)
    {
        startPosition = startPos;
        endPosition = endPos;

        Vector2Int startIntPosition = new Vector2Int(
            Mathf.RoundToInt(startPosition.x),
            Mathf.RoundToInt(startPosition.y)
        );

        Vector2Int endIntPosition = new Vector2Int(
            Mathf.RoundToInt(endPosition.x),
            Mathf.RoundToInt(endPosition.y)
        );

        path = Pathfinding.pathfinding.Launch(startIntPosition, endIntPosition, TroopTypeToGrid[troopType]);

        if (path == null || path.Count == 0)
            return;

        tweenEnCours = 0;

        Rotate();
        StartTween(path[0]);
    }

    protected void Rotate()
    {
        if (path == null)
            return;

        Vector3 direction = path[tweenEnCours] - Vector3Int.RoundToInt(transform.position);

        if (direction == Vector3.zero)
            return;

        float alpha = MathF.Atan2(direction.y, direction.x) * 180 / Mathf.PI;

        transform.rotation = Quaternion.Euler(startPosition);
        transform.Rotate(0, 180, -alpha);
    }
}
