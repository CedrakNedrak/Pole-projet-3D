using UnityEngine;

public class TownData : MonoBehaviour
{
    [SerializeField] private string townName = "Cosy Cave";
    public string TownName => townName;
    public GameObject townUICanvas { get; private set; }
    public TownUI townUI { get; private set; }
    [SerializeField] private TownLevelingSystem townLevelingSystem;
    public TownLevelingSystem TownLevelingSystem => townLevelingSystem;
    [SerializeField] private TownGenerateGold townGenerateGold;
    public TownGenerateGold TownGenerateGold => townGenerateGold;
    [SerializeField] private SpawnMiner spawnMiner;
    public SpawnMiner SpawnMiner => spawnMiner;
    public Ressources ressources { get; private set; }
    [SerializeField] private BuildTown buildTown;
    public BuildTown BuildTown => buildTown;
    public CameraMovement cameraMovement { get; private set; }

    public void SetTownName(string name)
    {
        townName = name;
    }

    public void Initialize(string name, int goldIncome, int level, int buildPrice, int minerPrice, int upgradePrice)
    {
        ressources = FindAnyObjectByType<Ressources>();
        SetTownName(name);
        townGenerateGold.SetGoldIncome(goldIncome);
        townLevelingSystem.SetTownLevel(level);
        buildTown.SetBuildPrice(buildPrice);
        SpawnMiner.SetMinerPrice(minerPrice);
        TownLevelingSystem.SetUpgradePrice(upgradePrice);
    }

    public void InitializeReferences(GameObject townUICanvas, TownUI townUI, CameraMovement camera)
    {
        this.townUICanvas = townUICanvas;
        this.townUI = townUI;
        cameraMovement = camera;

    }
}
