
using System.Collections.Generic;
using UnityEngine;

public class TweenManager : MonoBehaviour
{
    static List<Tween> tweens = new List<Tween>();
    static HashSet<Tween> tweensToPause = new HashSet<Tween>();

    public static int NumberOfTweens() { return tweens.Count; }
    public static void PausedTheTween(Tween tween) { tweensToPause.Add(tween); }
    public static void Add(Tween tween){ tweens.Add(tween); }

    void Update()
    {
        for (int i = tweens.Count - 1; i >= 0; i--)
        {
            if (tweensToPause.Contains(tweens[i])) { continue; }

            if (tweens[i].Update())
            {
                tweens.RemoveAt(i);
            }
        }

        // Nettoyer les tweens en pause après la boucle
        foreach (var tween in tweensToPause)
        {
            tweens.Remove(tween);
        }
        tweensToPause.Clear();
    }

}
