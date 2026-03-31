using System;
using UnityEngine;

public class Tween
{
    float duration;
    float time;
    Action<float> onUpdate;
    Action onComplete;

    public Tween(float duration, Action<float> onUpdate, Action onComplete = null)
    {
        this.duration = duration;
        this.onUpdate = onUpdate;
        this.onComplete = onComplete;
        time = 0f;
    }

    public bool Update()
    {
        time += Time.deltaTime;
        float t = Mathf.Clamp01(time / duration);

        onUpdate?.Invoke(t);

        if (time >= duration)
        {
            onComplete?.Invoke();
            return true; 
        }
        return false;
    }
}