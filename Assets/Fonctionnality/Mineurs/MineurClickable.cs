using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class MineurClickable : Clickable
{
    [SerializeField] private GameObject cursor;
    [SerializeField] private float speed = 0.1f;

    private bool stopCoroutine = false;
    private Vector3 endPosition;
    private List<int> indexTween = new List<int>();

    public float Speed { get => speed; set => speed = value; }

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

        Quaternion offset = Quaternion.Euler(0, 180, -90);

        Vector3 direction = endPosition - transform.position;
        direction.z= 0f;
        Quaternion rotation = Quaternion.LookRotation(direction);
        transform.rotation = rotation * offset;
        transform.rotation = Quaternion.Euler(0,180,transform.rotation.z);
        

        transform.LookAt(endPosition);
        StartTween();
        cursor.SetActive(false);
        stopCoroutine = false;
    }

    private void OnDisable() { MineurAction.OnCollision -= StopTween; }

    public void StopTween()
    {
        for (int i = 0; i < indexTween.Count; i++)
        {
            TweenManager.PausedTheTween(indexTween[i]);
        }
        Debug.Log("Hey");
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
