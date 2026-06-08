using UnityEngine;

public class GameplayManager : MonoBehaviour
{
    [SerializeField] private TownData townPrefab;
    [SerializeField] private GameObject townUICanvas;
    [SerializeField] private TownUI townUI;
    [SerializeField] private CameraMovement camera;

    void Start()
    {
    }

    public void SpawnTown(Vector3 position, Quaternion rotation, string name, int startGold, int goldIncome, int townLevel, int buildPrice, int minerPrice, int upgradePrice, string initialTownName = "Abandoned Ruin")
    {
        TownData newTown = Instantiate(townPrefab, position, rotation);
        newTown.InitializeReferences(townUICanvas, townUI, camera);
        newTown.Initialize(name, startGold, goldIncome, townLevel, buildPrice, minerPrice, upgradePrice, initialTownName);
    }
}
