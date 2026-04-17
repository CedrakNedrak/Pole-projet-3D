using UnityEngine;

public class TownClickable : Clickable
{
    [SerializeField] private TownData townData;

    public override void OnClick()
    {
        townData.townUICanvas.SetActive(true);
        townData.townUI.Initialize(townData);
        townData.cameraMovement.DisableCameraMovement();
    }
}
