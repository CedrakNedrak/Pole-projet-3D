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
        Vector3Int cell = new Vector3Int(
            Mathf.RoundToInt(position.x),
            Mathf.RoundToInt(position.y),
            0
        );

        discoveryManager.Register(
            cell,
            townPrefab.gameObject,
            rotation
        );
    }
}
