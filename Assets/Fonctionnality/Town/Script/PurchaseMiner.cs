using UnityEngine;

public class PurchaseMiner : MonoBehaviour
{
    [SerializeField] private TownUI townUI;

    public void Purchase()
    {
        townUI.townData.SpawnMiner.Spawn();
    }
}
