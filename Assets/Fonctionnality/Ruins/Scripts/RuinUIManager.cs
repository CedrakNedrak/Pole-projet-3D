using UnityEngine;

public class RuinClickable : Clickable
{
    [SerializeField] private GameObject canvas;
    //Whenever the object is clicked*
    public override void OnClick()
    {
        canvas.SetActive(true);
    }

}
