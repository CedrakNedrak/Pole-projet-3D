using UnityEngine;

public class TownData : MonoBehaviour
{
    [SerializeField] private string townName = "Cosy Cave";
    public string TownName => townName;
    private string initialTownName;
    public string InitialTownName => initialTownName;
    public GameObject townUICanvas { get; private set; }
    public TownUI townUI { get; private set; }
    [SerializeField] private TownLevelingSystem townLevelingSystem;
    public TownLevelingSystem TownLevelingSystem => townLevelingSystem;
    [SerializeField] private TownGenerateGold townGenerateGold;
    public TownGenerateGold TownGenerateGold => townGenerateGold;
    [SerializeField] private SpawnMiner spawnMiner;
    public SpawnMiner SpawnMiner => spawnMiner;
    [SerializeField] private SpawnSoldier spawnSoldier;
    public SpawnSoldier SpawnSoldier => spawnSoldier;
    public Ressources ressources { get; private set; }
    [SerializeField] private BuildTown buildTown;
    public BuildTown BuildTown => buildTown;
    [SerializeField] private GameObject[] townsLevelSprite;
    public GameObject[] TownsLevelSprite => townsLevelSprite;
    public CameraMovement cameraMovement { get; private set; }

    public void SetTownName(string name)
    {
        townName = name;
    }

    public void Initialize(string townName, int startGold, int goldIncome, int level, int buildPrice, int minerPrice, int upgradePrice, string initialTownName = "Abandoned Ruin")
    {
        ressources = FindAnyObjectByType<Ressources>();
        this.initialTownName = initialTownName;
        SetTownName(townName);
        townLevelingSystem.SetTownLevel(level);
        TownsLevelSprite[level].SetActive(true);
        TownLevelingSystem.SetUpgradePrice(upgradePrice);
        townGenerateGold.SetGoldIncome(goldIncome);
        townGenerateGold.SetStartGold(startGold);
        buildTown.SetBuildPrice(buildPrice);
        SpawnMiner.SetMinerPrice(minerPrice);
    }

    public void InitializeReferences(GameObject townUICanvas, TownUI townUI, CameraMovement camera)
    {
        this.townUICanvas = townUICanvas;
        this.townUI = townUI;
        cameraMovement = camera;

    }
}
