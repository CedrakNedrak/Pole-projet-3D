using System;
using System.Collections;
using System.Threading;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MineurAction : MonoBehaviour
{
    [SerializeField] private MineurClickable mineurClickable;
    [SerializeField] private float miningSpeed = 0.5f;
    [SerializeField] private int miningRange = 2;
    private bool isMining;
    private Tilemap tilemap;

    public static event Action OnCollision ;

    public void OnCollisionEnter(Collision collision)
    {
        collision.gameObject.SetActive(false);
        OnCollision?.Invoke();
    }

}
