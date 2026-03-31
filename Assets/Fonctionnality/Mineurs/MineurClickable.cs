using System.Collections;
using System.Runtime.CompilerServices;
using UnityEngine;

public class MineurClickable : Clickable
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] private GameObject cursor;
    private bool stopCoroutine = false;
    [SerializeField] private float speed =0.1f;
    private Vector3 endPosition;
    public float Speed { get => speed; set => speed = value; }

    public override void OnClick() {
        cursor.SetActive(true);
        StartCoroutine(WaitAnOtherClick());
    }
    public void OnMouseDown()
    {
        cursor.SetActive(true);
        StartCoroutine(WaitAnOtherClick());
    }
    public IEnumerator WaitAnOtherClick()
    {
        while (!stopCoroutine)
        {
            if (Input.GetMouseButtonDown(1))
            {
                stopCoroutine = true;
            }
            yield return null;
        }



        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        Vector3 worldPosition = transform.position ;
        if (Physics.Raycast(ray, out hit))
        {
            worldPosition = hit.point;
        }
        worldPosition.z = 0;

        stopCoroutine = true;
        endPosition = worldPosition;

        Motion.CreateMotion(gameObject, endPosition, (transform.position - endPosition).magnitude*speed);
        cursor.SetActive(false);
        stopCoroutine = false;
    }

    public void StopMotion()
    {
        if(gameObject.TryGetComponent(out UpdateMotion updateMotion))
        {
            Destroy(updateMotion);
        }
    }

    public void RestartMotion()
    {
        Motion.CreateMotion(gameObject, endPosition, (transform.position - endPosition).magnitude*speed);
    }

    public void ChangeSpeed(float newSpeed)
    {
        StopMotion();
        speed = newSpeed;
        RestartMotion();
    }
}
