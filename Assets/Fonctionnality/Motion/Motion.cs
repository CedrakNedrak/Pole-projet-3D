using System;
using UnityEngine;

public static class Motion 
{
    public static void CreateMotion(GameObject motion, Vector3 endPosition, float time)
    {
        UpdateMotion upMotion = motion.AddComponent<UpdateMotion>();
        upMotion.Init(endPosition, time);
    }

}
