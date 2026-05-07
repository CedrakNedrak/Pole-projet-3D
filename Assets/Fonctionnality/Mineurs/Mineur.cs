using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mineur : CharaMovement
{
    [SerializeField] private float pauseWhenMinining = 0.5f;
    [SerializeField] private int miningRange = 2;

    [SerializeField] private GameObject cursor;
    [SerializeField] private GameObject tilemapManager;
    
    private FogOfWar fogOfWar;

    private Vector3 endPosition;
    private Vector3 startPosition = new Vector3(0, 2, 0);

    private void OnDisable()
    {
        changeTween -= ChangeTween;
    }

    private void Start()
    {
        if (tilemapManager != null)
            fogOfWar = tilemapManager.GetComponent<FogOfWar>();
    }

    private void ChangeTween()
    {
        if (tweenEnCours < path.Count - 1)
        {
            tweenEnCours += 1;
            StartTween(path[tweenEnCours]);
        }
        else
        {
            tweenEnCours = 0;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Map"))
        {
            StartCoroutine(WaitBeforeDestroying(collision.gameObject));
        }
    }

    public void StartMining()
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
        path = Pathfinding.pathfinding.Launch(startIntPosition, endIntPosition);

        if (path == null || path.Count == 0)
            return;

        tweenEnCours = 0;

        Rotate();
        StartTween(path[0]);

        if (cursor != null)
            cursor.SetActive(false);
    }

    private IEnumerator WaitBeforeDestroying(GameObject collision)
    {
        StopTween();

        yield return new WaitForSeconds(pauseWhenMinining);
        collision.SetActive(false);
        TileGenerator.tileGenerator.WorldIntMatrice[(int)collision.transform.position.x, (int)collision.transform.position.y] = 1;

        Vector3Int cell = new Vector3Int(
            Mathf.RoundToInt(collision.transform.position.x),
            Mathf.RoundToInt(collision.transform.position.y),
            0
        );

        TileGenerator.tileGenerator.DigCell(cell);

        if (fogOfWar != null)
            fogOfWar.RefreshVisibility();

        if (path != null && path.Count > tweenEnCours)
        {
            Rotate();
            StartTween(path[tweenEnCours]);
        }
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
}