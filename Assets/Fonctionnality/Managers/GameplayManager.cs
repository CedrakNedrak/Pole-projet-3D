using UnityEngine;

public class GameplayManager : MonoBehaviour
{
    [SerializeField] private TownData townPrefab;
    [SerializeField] private GameObject townUICanvas;
    [SerializeField] private TownUI townUI;
    [SerializeField] private CameraMovement camera;
    [SerializeField] private DiscoveryManager discoveryManager;

    void Start()
    {
    }

    public void SpawnTown(Vector3 position, Quaternion rotation, string name, int startGold, int goldIncome, int townLevel, int buildPrice, int minerPrice, int upgradePrice, string initialTownName = "Abandoned Ruin")
    {
        discoveryManager.Register(position, townPrefab, rotation);
    }
}
