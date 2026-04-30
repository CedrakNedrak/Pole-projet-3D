using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Mineur : MonoBehaviour
{
    [SerializeField] private float pauseWhenMinining = 0.5f;
    [SerializeField] private int miningRange = 2;

    [SerializeField] private GameObject cursor;
    [SerializeField] private float speed = 0.1f;

    private Vector3 endPosition;
    private Vector3 startPosition = new Vector3(0, 2, 0);
    private List<int> indexTween = new List<int>();
    List<Vector3Int> path = new List<Vector3Int>();
    Action changeTween;
    private int tweenEnCours;
    public void OnEnable()
    {
        tweenEnCours = 0;
        changeTween += ChangeTween;
    }

    private void ChangeTween()
    {
        if (tweenEnCours < path.Count-1)
        {
            tweenEnCours += 1;
            StartTween(path[tweenEnCours]);
        }
        else { tweenEnCours = 0; }
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Map"))
        {
            StartCoroutine(WaitBeforeDestroying(collision.gameObject));
        }
    }

    public void StartMining()
    {
        TakeEndPosition();
        Vector2Int startIntPosition = new Vector2Int((int)transform.position.x, (int)transform.position.y);
        Vector2Int endIntPosition = new Vector2Int((int)endPosition.x, (int)endPosition.y);

        path = Pathfinding.pathfinding.Launch(startIntPosition, endIntPosition);

        Rotate();
        StartTween(path[0]);
        cursor.SetActive(false);
    }

    public IEnumerator WaitBeforeDestroying(GameObject collision)
    {
        StopTween();
        yield return new WaitForSeconds(pauseWhenMinining);
        collision.SetActive(false);
        TileGenerator.tileGenerator.WorldIntMatrice[(int)collision.transform.position.x, (int)collision.transform.position.y] = 1;
        if (path.Count >= 1)
            Rotate();
            StartTween(path[tweenEnCours]);
    }
    
    public void StopTween()
    {
        for (int i = 0; i < indexTween.Count; i++)
        {
            TweenManager.PausedTheTween(indexTween[i]);
        }
    }

    public void StartTween(Vector3Int end)
    {
        Vector3 beginingPosition = transform.position;
        float time = (end - beginingPosition).magnitude / speed;

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
        Vector3 direction = path[tweenEnCours] - transform.position;
        float alpha = MathF.Atan2(direction.y, direction.x) * 180 / Mathf.PI;
        transform.rotation = Quaternion.Euler(startPosition);
        transform.Rotate(0, 180, -alpha);
    }
}

