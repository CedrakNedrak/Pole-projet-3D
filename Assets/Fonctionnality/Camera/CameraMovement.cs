using UnityEngine;
using UnityEngine.InputSystem;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] float verticalScreenRatioForCameraMovement;
    [SerializeField] float horizontalScreenRatioForCameraMovement;

    private Vector2 scrollSpeed = Vector2.zero;
    [SerializeField] private float edgeScrollMaxSpeed;
    [SerializeField] private float edgeScrollAccelSpeed;
    [SerializeField] private float edgeScrollDecelSpeed;
    private float zoomSpeed = 0f;
    [SerializeField] private float zoomMaxSpeed;
    [SerializeField] private float zoomAccelSpeed;
    [SerializeField] private float zoomDecelSpeed;

    [SerializeField] private float minZoom;
    [SerializeField] private float maxZoom;


    [SerializeField] Transform cameraTransform;
    [SerializeField] Camera camera;

    private Vector3 lastDirection = Vector3.zero;

    private bool isCameraMovementEnabled = true;
    public bool IsCameraMovementEnabled => isCameraMovementEnabled;

    private Vector3 EdgeMovement(Vector3 lastDirection)
    {
        bool up = false;
        bool down = false;
        bool horizontal = false;
        bool right = false;
        bool left = false;
        bool vertical = false;
        (up, down, horizontal, right, left, vertical) = whereMousePos();

        Vector3 presentDirection = lastDirection;//by default, keep the same direction as before
        Vector3 lastDirectionX = new Vector3(lastDirection.x, 0, 0);
        Vector3 lastDirectionY = new Vector3(0, lastDirection.y, 0);

        if (left)//screen width not set in start to be able to resize game window
        {
            scrollSpeed.x = Mathf.Lerp(scrollSpeed.x, edgeScrollMaxSpeed, Time.deltaTime * edgeScrollAccelSpeed);
            cameraTransform.Translate(Vector3.left * scrollSpeed.x * Time.deltaTime);
            presentDirection.x = -1;
        }
        else if (right)
        {
            scrollSpeed.x = Mathf.Lerp(scrollSpeed.x, edgeScrollMaxSpeed, Time.deltaTime * edgeScrollAccelSpeed);
            cameraTransform.Translate(Vector3.right * scrollSpeed.x * Time.deltaTime);
            presentDirection.x = +1;
        }
        else
        {
            scrollSpeed.x = Mathf.Lerp(scrollSpeed.x, 0, Time.deltaTime * edgeScrollDecelSpeed);
            cameraTransform.Translate(lastDirectionX * scrollSpeed.x * Time.deltaTime);
        }

        if (down)
        {
            scrollSpeed.y = Mathf.Lerp(scrollSpeed.y, edgeScrollMaxSpeed, Time.deltaTime * edgeScrollAccelSpeed);
            cameraTransform.Translate(Vector3.down * scrollSpeed.y * Time.deltaTime);
            presentDirection.y = -1;
        }
        else if (up)
        {
            scrollSpeed.y = Mathf.Lerp(scrollSpeed.y, edgeScrollMaxSpeed, Time.deltaTime * edgeScrollAccelSpeed);
            cameraTransform.Translate(Vector3.up * scrollSpeed.y * Time.deltaTime);
            presentDirection.y = +1;
        }
        else
        {
            scrollSpeed.y = Mathf.Lerp(scrollSpeed.y, 0, Time.deltaTime * edgeScrollDecelSpeed);
            cameraTransform.Translate(lastDirectionY * scrollSpeed.y * Time.deltaTime);
        }

        return presentDirection;
    }

    private (bool, bool, bool, bool, bool, bool) whereMousePos()//up, down, horizontal, right, left, vertical
    {
        Vector2 mousePos = Mouse.current.position.ReadValue();
        float mousePosX = mousePos.x;
        float mousePosY = mousePos.y;

        //screen width not set in start to be able to resize game window
        bool left = (mousePosX < (1f - horizontalScreenRatioForCameraMovement) * Screen.width);
        bool right = (mousePosX >= horizontalScreenRatioForCameraMovement * Screen.width);
        bool vertical = !(left) && !(right);
        bool down = (mousePosY < (1f - verticalScreenRatioForCameraMovement) * Screen.height);
        bool up = (mousePosY >= verticalScreenRatioForCameraMovement * Screen.height);
        bool horizontal = !(up) && !(down);
        return (up, down, horizontal, right, left, vertical);
    }

    private void Zoom()
    {
        //zoom speed > 0 <=> zoom in
        float mouseScroll = Mouse.current.scroll.ReadValue().y;
        if (mouseScroll > 0)
        {
            zoomSpeed = Mathf.Lerp(zoomSpeed, +zoomMaxSpeed, Time.deltaTime * zoomAccelSpeed);
        }
        else if (mouseScroll < 0)
        {
            zoomSpeed = Mathf.Lerp(zoomSpeed, -zoomMaxSpeed, Time.deltaTime * zoomAccelSpeed);
        }
        else
        {
            zoomSpeed = Mathf.Lerp(zoomSpeed, 0, Time.deltaTime * zoomDecelSpeed);
        }
        camera.orthographicSize = Mathf.Clamp(camera.orthographicSize - zoomSpeed * Time.deltaTime, minZoom, maxZoom);
    }

    public void DisableCameraMovement()
    {
        isCameraMovementEnabled = false;
        StopCamera();
    }

    public void EnableCameraMovement()
    {
        isCameraMovementEnabled = true;
    }

    private void StopCamera()
    {
        lastDirection = Vector3.zero;
        scrollSpeed = Vector2.zero;
        zoomSpeed = 0f;
    }

    private void Update()
    {
        if (isCameraMovementEnabled)
        {
            lastDirection = EdgeMovement(lastDirection);
            Zoom();
        }
    }
}
