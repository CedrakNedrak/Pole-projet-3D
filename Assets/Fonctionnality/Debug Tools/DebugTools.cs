using UnityEngine;
using UnityEngine.InputSystem;

public class DebugTools : MonoBehaviour
{
    //[SerializeField] private Key toggleCameraMovement;
    [SerializeField] private CameraMovement camera;
    [SerializeField] private GameObject golem;
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

        if (Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            Vector2 pos = TileGenerator.tileGenerator.MainTownPosition;
            int z = (int)(Mathf.Tan(-40 * Mathf.PI / 180) * pos.y) - 10;
            Camera.main.transform.position = new Vector3(pos.x, pos.y, z);
        }

        if (Keyboard.current.cKey.wasPressedThisFrame)
        {
            GameTimer timer = GameTimer.instance;
            timer.Temps += 60;
        }
    }

    private void Start()
    {
        Instantiate(golem, new Vector3(100, 101, 0), Quaternion.Euler(0f, -90f, 0f) * Quaternion.Euler(0f, 0f, 90f));
    }
}
