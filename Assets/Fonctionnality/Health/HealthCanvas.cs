using Mono.Cecil;
using UnityEngine;

public class HealthCanvas : MonoBehaviour
{
    private static Vector3 targetRotation = new Vector3(0f,0f,0f);
    private static Quaternion quadTargetRotation = Quaternion.Euler(targetRotation);
    
    void LateUpdate()
    {
        transform.rotation = quadTargetRotation;
    }
}
