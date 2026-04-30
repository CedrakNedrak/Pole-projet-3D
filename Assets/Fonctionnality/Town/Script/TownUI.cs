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
    private int townLevel;

    public void Initialize(TownData townData)
    {
        this.townData = townData;
        SetTownLevel(townData.TownLevelingSystem.TownLevel);
        SetGoldIncome(townData.TownGenerateGold.GoldIncome);
        SetMinerPrice(townData.SpawnMiner.MinerPrice);
        SetUpgradePrice(townData.TownLevelingSystem.UpgradePrice);
        townLevel = townData.TownLevelingSystem.TownLevel;
        if (townLevel == 0)
        {
            SetTownName(townData.InitialTownName);
            DisableSpawn();
            SetUpgradeText("Renovate Ruin", townData.TownLevelingSystem.UpgradePrice);
            upgradeGameObject.transform.localPosition = new Vector3(0, -360, 0);
            EnableUpgrade();
        }
        else
        {
            SetTownName(townData.TownName);
            EnableSpawn();
            SetUpgradeText("Upgrade Town", townData.TownLevelingSystem.UpgradePrice);
            upgradeGameObject.transform.localPosition = new Vector3(600, -360, 0);
            EnableUpgrade();
        }
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

    public void SetUpgradeText(string upgradeText, int upgradePrice)
    {
        upgradeTownText.text = upgradeText + "\n(" + upgradePrice.ToString() + " G)";
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
