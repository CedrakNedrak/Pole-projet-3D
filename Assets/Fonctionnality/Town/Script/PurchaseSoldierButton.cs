using UnityEngine;

public class PurchaseSoldierButton : MonoBehaviour
{
    [SerializeField] private TownUI townUI;
    public void PurchaseSoldier()
    {
        townUI.townData.SpawnSoldier.Spawn();
    }

}

