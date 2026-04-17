using UnityEngine;

public class GameplayManager : MonoBehaviour
{
    [SerializeField] private TownData townPrefab;
    [SerializeField] private GameObject townUICanvas;
    [SerializeField] private TownUI townUI;
    [SerializeField] private CameraMovement camera;

    void Start()
    {
        TownData firstTown = Instantiate(townPrefab, new Vector3(-2.883264f, -1.516974f, 0.4262036f), Quaternion.identity);
        firstTown.InitializeReferences(townUICanvas, townUI, camera);
        firstTown.Initialize("Cosy Cave", 70, 1, 0, 40, 1000);
    }
}
