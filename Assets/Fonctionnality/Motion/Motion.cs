using System;
using System.Collections;
using UnityEngine;

public static class Motion 
{
    public static IEnumerator Tween(System.Action<float> onUpdate, float duration)
    {
        float time = 0f;

        while (time < duration)
        {
            time += Time.deltaTime;
            float t = time / duration;

            onUpdate?.Invoke(t);

            yield return null;
        }

        onUpdate?.Invoke(1f);
    }

   
}
