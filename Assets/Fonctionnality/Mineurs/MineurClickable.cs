using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;

public class MineurClickable : Clickable
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] private GameObject cursor;
    private bool stopCoroutine = false;
    [SerializeField] private float speed =0.1f;
    private Vector3 endPosition;
    public float Speed { get => speed; set => speed = value; }
    private List<int> indexTween = new List<int>();

    private void OnEnable() { MineurAction.OnCollision += StopTween; }

    public override void OnClick() {
        cursor.SetActive(true);
        StartCoroutine(WaitAnOtherClick());
    }

    public IEnumerator WaitAnOtherClick()
    {
        while (!stopCoroutine)
        {
            if (Input.GetMouseButtonDown(1))
            {
                stopCoroutine = true;
            }
            yield return null;
        }

        TakeEndPosition();

        Vector3 beginingPosition = transform.position ;
       
        TweenManager.Add(new Tween(1f, t =>
        {
            transform.position = Vector3.Lerp(beginingPosition, endPosition, t);
        }));

        indexTween.Add(TweenManager.NumberOfTweens()-1);

        cursor.SetActive(false);
        stopCoroutine = false;
    }

    private void OnDisable() { MineurAction.OnCollision -= StopTween; }

    void StopTween()
    {
        for (int i = 0; i < indexTween.Count; i++)
        {
            TweenManager.PausedTheTween(indexTween[i]);
        }
        Debug.Log("Hey");
    }

    private void TakeEndPosition()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        Vector3 worldPosition = transform.position;
        if (Physics.Raycast(ray, out hit))
        {
            worldPosition = hit.point;
        }
        worldPosition.z = 0;
        endPosition = worldPosition;
    }
}
