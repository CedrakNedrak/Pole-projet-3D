using Unity.VisualScripting;
using UnityEngine;

public class TownClickable : Clickable
{
    [SerializeField] private GameObject canvas;
    [SerializeField] private CameraMovement camera;
    // Whenever the object is clicked
    public override void OnClick()
    {
        canvas.SetActive(true);
        camera.DisableCameraMovement();
    }

}
