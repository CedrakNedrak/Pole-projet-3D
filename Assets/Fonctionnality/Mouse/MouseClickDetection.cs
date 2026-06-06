using UnityEngine;
using UnityEngine.UIElements;
using static UnityEngine.UI.Image;

public class MouseClickDetection : MonoBehaviour
{

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            RaycastHit[] hits = Physics.RaycastAll(ray, Mathf.Infinity);
            foreach (var h in hits)
            {
                GameObject clickedObject = h.collider.gameObject;
                if (clickedObject.TryGetComponent(out Clickable clickableObject))
                {
                    clickableObject.OnClick();
                    break;
                }
            }
        }
        
    }
}
