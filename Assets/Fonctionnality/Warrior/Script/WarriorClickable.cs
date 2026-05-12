using UnityEngine;

public class WarriorClickable : Clickable
{
    [SerializeField] private GameObject cursor;
    [SerializeField] private WarriorMovement warrior;

    private bool firstClick;

    public override void OnClick()
    {
        cursor.SetActive(true);
        firstClick = true;
        Debug.Log("aaaa");
    }

    public void Update()
    {
        if (firstClick)
        {
            if (Input.GetMouseButtonDown(1))
            {
                warrior.Move();
                firstClick = false;
                cursor.SetActive(false);
            }
        }
    }
}
