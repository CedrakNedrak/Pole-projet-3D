using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MineurClickable : Clickable
{
    [SerializeField] private GameObject cursor;
    [SerializeField] private Mineur mineur;

    private bool firstClick;

    public override void OnClick() {
        cursor.SetActive(true);
        firstClick = true;
        Debug.Log("Right click detected, starting mining.");
    }

    public void Update()
    {
        if (firstClick)
        {
            if (Input.GetMouseButtonDown(1))
            {
                mineur.StartMining();
                firstClick = false;
                cursor.SetActive(false);
            }
        }
    }
 
}
