
using System.Collections.Generic;
using UnityEngine;

public class TweenManager : MonoBehaviour
{
    static List<Tween> tweens = new List<Tween>();
    static List<int> tweensPaused = new List<int>();

    private static bool IsPaused(int index) { return tweensPaused.Contains(index); }
    public static int NumberOfTweens() { return tweens.Count; }
    public static void PausedTheTween(Tween tween) { tweens.Remove(tween);  }
    public static void Add(Tween tween){ tweens.Add(tween); }

    void Update()
    {
        for (int i = tweens.Count - 1; i >= 0; i--)
        {
            if (IsPaused(i)) { continue; }

            if (tweens[i].Update())
            {
                tweens.RemoveAt(i);
            }
        }
    }

}
