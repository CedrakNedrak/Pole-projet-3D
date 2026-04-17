using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TownUI : MonoBehaviour
{
    [SerializeField] private Button upgradeButton;
    [SerializeField] private TextMeshProUGUI goldIncomeText;
    [SerializeField] private TextMeshProUGUI townNameText;
    [SerializeField] private TextMeshProUGUI townLevelText;
    [SerializeField] private Button purchaseMinerButton;
    [SerializeField] private Button purchaseSoldierButton;
    [SerializeField] private TextMeshProUGUI purchaseMinerText;
    [SerializeField] private TextMeshProUGUI upgradeTownText;

    private GameObject upgradeGameObject;
    private GameObject purchaseSoldierGameObject;
    private GameObject purchaseMinerGameObject;

    public TownData townData { get; private set; }

    public void Initialize(TownData townData)
    {
        this.townData = townData;
        SetTownLevel(townData.TownLevelingSystem.TownLevel);
        SetTownName(townData.TownName);
        SetGoldIncome(townData.TownGenerateGold.GoldIncome);
        SetMinerPrice(townData.SpawnMiner.MinerPrice);
        SetUpgradePrice(townData.TownLevelingSystem.UpgradePrice);
    }

    private void SetUpgradePrice(int upgradePrice)
    {
        upgradeTownText.text = "Upgrade Town\n(" + upgradePrice.ToString() + " G)";
    }

    private void Awake()
    {
        upgradeGameObject = upgradeButton.gameObject;
        purchaseMinerGameObject = purchaseMinerButton.gameObject;
        purchaseSoldierGameObject = purchaseSoldierButton.gameObject;
    }

    public void DisableSpawn()
    {
        purchaseSoldierGameObject.SetActive(false);
        purchaseMinerGameObject.SetActive(false);
    }

    public void EnableSpawn()
    {
        purchaseSoldierGameObject.SetActive(true);
        purchaseMinerGameObject.SetActive(true);
    }

    public void SetTownName(string cityName)
    {
        townNameText.text = cityName;
    }

    public void SetTownLevel(int level)
    {
        townLevelText.text = "Level " + level;
    }

    public void SetGoldIncome(int goldIncome)
    {
        goldIncomeText.text = "+" + Mathf.Round(goldIncome).ToString() + "G/s";
    }
    public void DisableUpgrade()
    {
        upgradeGameObject.SetActive(false);
    }
    public void EnableUpgrade()
    {
        upgradeGameObject.SetActive(true);
    }

    public void SetMinerPrice(int price)
    {
        purchaseMinerText.text = "Purchase Miner\n(" + price.ToString() + " G)";
    }
}
