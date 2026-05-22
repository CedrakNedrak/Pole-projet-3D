using UnityEngine;

public class AllyTroopClickable : Clickable
{
    [SerializeField] private GameObject cursorCanvas;
    [SerializeField] private AllyTroopMovement troop;

    private bool firstClick;

    public override void OnClick()
    {
        cursorCanvas.SetActive(true);
        firstClick = true;
    }

    public void Update()
    {
        if (firstClick)
        {
            if (Input.GetMouseButtonDown(1))
            {
                troop.StartMoving();
                firstClick = false;
            }
        }
    }

}
