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
        TownData firstTown = Instantiate(townPrefab, new Vector3(-2.883264f, -1.516974f, 0.4262036f), Quaternion.identity);
        firstTown.InitializeReferences(townUICanvas, townUI, camera);
        firstTown.Initialize("Cosy Cave", 200, 70, 1, 0, 40, 1000);

        TownData firstRuin = Instantiate(townPrefab, new Vector3(-8.69502f, -1.45207f, 4.136147f), Quaternion.identity);
        firstRuin.InitializeReferences(townUICanvas, townUI, camera);
        firstRuin.Initialize("Miner Haven", 0, 40, 0, 200, 40, 1000);
    }
}
