using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mineur : MonoBehaviour
{
    [SerializeField] private float pauseWhenMinining = 0.5f;
    [SerializeField] private int miningRange = 2;

    [SerializeField] private GameObject cursor;
    [SerializeField] private FogOfWar fogOfWar;

    [SerializeField] private float speed = 0.1f;

    private Vector3 endPosition;
    private Vector3 startPosition = new Vector3(0, 2, 0);

    private List<int> indexTween = new List<int>();
    private List<Vector3Int> path = new List<Vector3Int>();

    private Action changeTween;
    private int tweenEnCours;

    private void OnEnable()
    {
        tweenEnCours = 0;
        changeTween += ChangeTween;
    }

    private void OnDisable()
    {
        changeTween -= ChangeTween;
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

        Vector3Int cell = new Vector3Int(
            Mathf.RoundToInt(collision.transform.position.x),
            Mathf.RoundToInt(collision.transform.position.y),
            0
        );

        TileGenerator.tileGenerator.DigCell(cell);

        if (fogOfWar != null)
            fogOfWar.RefreshVisibility();

        if (path.Count >= 1)
        collision.SetActive(false);
        TileGenerator.tileGenerator.WorldIntMatrice[(int)collision.transform.position.x, (int)collision.transform.position.y] = 1;
        if (path.Count > tweenEnCours)
        {
            Rotate();
            StartTween(path[tweenEnCours]);
        }
    }

    private void StopTween()
    {
        for (int i = 0; i < indexTween.Count; i++)
        {
            TweenManager.PausedTheTween(indexTween[i]);
        }
    }

    private void StartTween(Vector3Int end)
    {
        Vector3 beginingPosition = transform.position;
        float time = (end - Vector3Int.RoundToInt(beginingPosition)).magnitude / speed;

        TweenManager.Add(new Tween(time, t =>
        {
            transform.position = Vector3.Lerp(beginingPosition, end, t);
        }, changeTween));

        indexTween.Add(TweenManager.NumberOfTweens() - 1);
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