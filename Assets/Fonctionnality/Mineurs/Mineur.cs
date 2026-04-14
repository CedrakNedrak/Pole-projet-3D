using Codice.CM.Common;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Mineur : MonoBehaviour
{
    [SerializeField] private MineurClickable mineurClickable;
    [SerializeField] private float pauseWhenMinining = 0.5f;
    [SerializeField] private int miningRange = 2;

    [SerializeField] private GameObject cursor;
    [SerializeField] private float speed = 0.1f;

    private Vector3 endPosition;
    private Vector3 startPosition = new Vector3(0, 180, 0);
    private List<int> indexTween = new List<int>();

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
        Rotate();
        StartTween();
        cursor.SetActive(false);
    }

    public IEnumerator WaitBeforeDestroying(GameObject collision)
    {
        StopTween();
        yield return new WaitForSeconds(pauseWhenMinining);
        collision.SetActive(false);
        StartTween();
    }

    public void StopTween()
    {
        for (int i = 0; i < indexTween.Count; i++)
        {
            TweenManager.PausedTheTween(indexTween[i]);
        }
    }

    public void StartTween()
    {
        Vector3 beginingPosition = transform.position;
        float time = (endPosition - beginingPosition).magnitude / speed;

        TweenManager.Add(new Tween(time, t =>
        {
            transform.position = Vector3.Lerp(beginingPosition, endPosition, t);
        }));

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
        Vector3 direction = endPosition - transform.position;
        float alpha = MathF.Atan2(direction.y, direction.x) * 180 / Mathf.PI;
        transform.rotation = Quaternion.Euler(startPosition);
        transform.Rotate(0, 0, -alpha);
    }
}
