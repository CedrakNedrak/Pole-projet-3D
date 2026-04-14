using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MineurClickable : Clickable
{
    [SerializeField] private GameObject cursor;
    [SerializeField] private float speed = 0.1f;
    [SerializeField] private Mineur mineur;

    private bool stopCoroutine = false;
    public float Speed { get => speed; set => speed = value; }

    public event Action StartMining;
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
        mineur.StartMining();
        stopCoroutine = false;
    }
}
