using Unity.VisualScripting;
using UnityEngine;

public class TownClickable : Clickable
{
    [SerializeField] private GameObject canvas;
    // Whenever the object is clicked
   public override void OnClick()
    {
        canvas.SetActive(true);
    }
    
}
