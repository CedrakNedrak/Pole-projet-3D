using UnityEngine;

public class GameplayManager : MonoBehaviour
{
    [SerializeField] private TownData townPrefab;
    [SerializeField] private GameObject townUICanvas;
    [SerializeField] private TownUI townUI;
    [SerializeField] private CameraMovement camera;

    void Start()
    {
        //comment générer une ville
        //rq: ville de niveau 0 = ruine
        SpawnTown(new Vector3(-2.883264f, -1.516974f, 0.4262036f), Quaternion.identity, "Cosy Cave", 200, 70, 1, 0, 40, 1000);
        SpawnTown(new Vector3(-8.69502f, -1.45207f, 4.136147f), Quaternion.identity, "Miner Haven", 0, 40, 0, 200, 40, 1000);
    }

    public void SpawnTown(Vector3 position, Quaternion rotation, string name, int startGold, int goldIncome, int townLevel, int buildPrice, int minerPrice, int upgradePrice, string initialTownName = "Abandoned Ruin")
    {
        TownData newTown = Instantiate(townPrefab, position, rotation);
        newTown.InitializeReferences(townUICanvas, townUI, camera);
        newTown.Initialize(name, startGold, goldIncome, townLevel, buildPrice, minerPrice, upgradePrice, initialTownName);
    }
}
