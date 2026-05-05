using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mineur : CharaMovement
{
    [SerializeField] private float pauseWhenMinining = 0.5f;
    [SerializeField] private int miningRange = 2;

    [SerializeField] private GameObject cursor;

    private Vector3 endPosition;
    private Vector3 startPosition = new Vector3(0, 2, 0);

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
        if (path!=null && path.Count > tweenEnCours)
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
        Vector3 direction = path[tweenEnCours] - transform.position;
        float alpha = MathF.Atan2(direction.y, direction.x) * 180 / Mathf.PI;
        transform.rotation = Quaternion.Euler(startPosition);
        transform.Rotate(0, 180, -alpha);
    }
}

