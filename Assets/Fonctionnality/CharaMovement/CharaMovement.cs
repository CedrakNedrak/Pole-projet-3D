using System;
using System.Collections.Generic;
using UnityEngine;

public class CharaMovement : MonoBehaviour
{
    [SerializeField] protected float speed;
    protected List<Vector3Int> path;
    protected List<Tween> tweens = new List<Tween>();
    protected Action changeTween;
    protected int tweenEnCours;
    protected int tweenToStop;

    public void OnEnable()
    {
        tweenEnCours = 0;
        changeTween += ChangeTween;
    }

    public virtual void ChangeTween()
    {
        if (tweenEnCours < path.Count - 1)
        {
            tweenEnCours += 1;
            StartTween(path[tweenEnCours]);
        }
        else { tweenEnCours = 0; }
    }

    protected void StopTween()
    {
        foreach (var tween in tweens)
        {
            TweenManager.PausedTheTween(tween);
            tweens = new List<Tween>();
        }
    }

    protected void StartTween(Vector3Int end)
    {
        Vector3 beginingPosition = transform.position;
        float time = (end - beginingPosition).magnitude / speed;
        Tween tween = new Tween(time, t =>
        {
            transform.position = Vector3.Lerp(beginingPosition, end, t);
        }, changeTween);

        TweenManager.Add(tween);

        tweens.Add(tween);
    }
}
