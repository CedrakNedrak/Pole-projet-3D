using System;
using UnityEngine;

public class WarriorMovement : CharaMovement
{
    [SerializeField] private float pauseWhenMinining = 0.5f;
    [SerializeField] private int miningRange = 2;

    private FogOfWar fogOfWar;

    private Vector3 endPosition;
    private Vector3 startPosition = new Vector3(0, 2, 0);

    public void Move()
    {
        TakeEndPosition();

        Vector2Int startIntPosition = new Vector2Int(
            Mathf.RoundToInt(transform.position.x),
            Mathf.RoundToInt(transform.position.y)
        );

        Vector2Int endIntPosition = new Vector2Int(
            Mathf.RoundToInt(endPosition.x),
            Mathf.RoundToInt(endPosition.y)
        );
        if (Pathfinding.pathfinding == null)
        {
            Debug.LogError("[Mineur] Pathfinding.pathfinding est NULL. Vérifie que ton objet Pathfinding est bien dans la scène.");
            return;
        }
        ChangeWorldIntMatrice();

        path = Pathfinding.pathfinding.Launch(startIntPosition, endIntPosition);

        Debug.Log(TileGenerator.tileGenerator.WorldIntMatrice[endIntPosition.x, endIntPosition.y]);
            
        Pathfinding.pathfinding.Grid = TileGenerator.tileGenerator.WorldIntMatrice;
        if (path == null || path.Count == 0)
            return;

        tweenEnCours = 0;

        Rotate();
        StartTween(path[0]);
    }

    private void TakeEndPosition()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Vector3 worldPosition = transform.position;
        Plane plane = new Plane(Vector3.forward, Vector3.zero);

        if (plane.Raycast(ray, out float distance))
        {
            worldPosition = ray.GetPoint(distance);
        }

        worldPosition.z = 0;
        endPosition = worldPosition;
    }

    private void Rotate()
    {
        if (path == null || path.Count == 0)
            return;

        Vector3 direction = path[tweenEnCours] - Vector3Int.RoundToInt(transform.position);

        if (direction == Vector3.zero)
            return;

        float alpha = MathF.Atan2(direction.y, direction.x) * 180 / Mathf.PI;

        transform.rotation = Quaternion.Euler(startPosition);
        transform.Rotate(0, 180, -alpha);
    }

    private void ChangeWorldIntMatrice()
    {
        int[,] warriorIntMatrice = (int[,])TileGenerator.tileGenerator.WorldIntMatrice.Clone();
        for (int i = 0; i < TileGenerator.tileGenerator.Width; i++)
        {
            for (int j = 0; j < TileGenerator.tileGenerator.Length; j++)
            {
                if (warriorIntMatrice[i,j] == 2)
                {
                    warriorIntMatrice[i, j] = -1;
                }
            }
        }

        Pathfinding.pathfinding.Grid = warriorIntMatrice;
    }
}
