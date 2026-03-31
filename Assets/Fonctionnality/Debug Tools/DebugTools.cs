using UnityEngine;
using UnityEngine.InputSystem;

public class DebugTools : MonoBehaviour
{
    //[SerializeField] private Key toggleCameraMovement;
    [SerializeField] private CameraMovement camera;

    void Update()
    {
        if (Keyboard.current.zKey.wasPressedThisFrame)
        {
            if (camera.IsCameraMovementEnabled)
            {
                camera.DisableCameraMovement();
            }
            else
            {
                camera.EnableCameraMovement();
            }
        }
    }
}
