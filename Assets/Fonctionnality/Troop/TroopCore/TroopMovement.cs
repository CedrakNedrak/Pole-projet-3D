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
    protected Quaternion rotationToFaceRight;
    public static Dictionary<TroopMovement.TroopType, int[,]> TroopTypeToGrid { set; get; }
    protected Vector3 nextEndPos;
    private bool endTweens;
    protected bool isMoving;

    protected virtual void Start()
    {
        isMoving = false;
        endTweens = false;
        TroopTypeToGrid = new Dictionary<TroopMovement.TroopType, int[,]>
        {
            { TroopMovement.TroopType.MiningTroop, TileGenerator.tileGenerator.MiningWorldIntMatrice },
            { TroopMovement.TroopType.NormalTroop, TileGenerator.tileGenerator.NormalWorldIntMatrice },
        };
    }

    public TroopMovement(TroopType troopType, Quaternion rotationToFaceRight)
    {
        this.troopType = troopType;
        this.rotationToFaceRight = rotationToFaceRight;
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
        isMoving = true;
    }

    protected void Rotate()
    {
        if (path == null || tweenEnCours >= path.Count)
            return;

        Vector3 direction = Vector3.zero;
        if (tweenEnCours > 0)
        {
            direction = path[tweenEnCours] - path[tweenEnCours - 1];
        }
        else
        {
            direction = path[tweenEnCours] - Vector3Int.RoundToInt(transform.position);
        }


        if (direction == Vector3.zero)
            return;

        Quaternion rotation = Quaternion.AngleAxis(Vector2.SignedAngle(Vector2.right, direction), Vector3.forward) * rotationToFaceRight;
        transform.rotation = rotation;
    }
    public override void ChangeTween()
    {
        if (!endTweens)
        {
            if (tweenEnCours < path.Count - 1)
            {
                tweenEnCours += 1;
                StartTween(path[tweenEnCours]);
                Rotate();
            }
            else { tweenEnCours = 0; isMoving = false; }
        }
        else
        {
            endTweens = false;
            StopTween();
            StartMoving(transform.position, nextEndPos);
        }

    }

    public void EndTweensThenStartMovingTowards(Vector3 nextEndPos)
    {
        endTweens = true;
        this.nextEndPos = nextEndPos;
        if (!isMoving)
        {
            StartMoving(transform.position, nextEndPos);
        }
    }
}