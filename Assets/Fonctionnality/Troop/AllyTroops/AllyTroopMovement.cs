using UnityEngine;

public class AllyTroopMovement : TroopMovement
{
    [SerializeField] private GameObject cursorCanvas;

    public AllyTroopMovement(TroopType troopType) : base(troopType) { }

    private Vector3 TakeEndPosition()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Vector3 worldPosition = transform.position;
        Plane plane = new Plane(Vector3.forward, Vector3.zero);

        if (plane.Raycast(ray, out float distance))
        {
            worldPosition = ray.GetPoint(distance);
        }

        worldPosition.z = 0;
        return worldPosition;
    }

    public void StartMoving()
    {
        Vector3 endPos = TakeEndPosition();
        base.StartMoving(transform.position, endPos);

        if (cursorCanvas != null)
        {
            cursorCanvas.SetActive(false);
        }
    }
}
