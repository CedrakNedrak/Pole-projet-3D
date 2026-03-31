using UnityEngine;
using UnityEngine.UIElements;

public class MouseClickDetection : MonoBehaviour
{

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, Mathf.Infinity))
            {
                GameObject clickedObject = hit.collider.gameObject;
                if (clickedObject.TryGetComponent(out Clickable clickableObject))
                {

                    clickableObject.OnClick();
                }
            }
        }
        
    }
}
