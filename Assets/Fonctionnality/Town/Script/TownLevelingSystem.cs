using UnityEngine;

public class TownLevelingSystem : MonoBehaviour
{
    [SerializeField] private TownData townData;
    [SerializeField] private int upgradePrice = 5;
    public int UpgradePrice => upgradePrice;
    [SerializeField] private int townLevel = 1;
    public int TownLevel => townLevel;

    public void UpgradeTown()
    {
        if (townData.ressources.SpendGold(upgradePrice))
        {
            SetTownLevel(townLevel + 1);

            foreach (var item in townData.TownsLevelSprite)
            {
                item.SetActive(false);
            }

            townData.TownsLevelSprite[townLevel].SetActive(true);
        }
    }
    public void SetTownLevel(int level)
    {
        townLevel = level;
        if (townData.townUI.gameObject.activeSelf)
        {
            townData.townUI.Initialize(townData);//update the UI, only happens when town is upgraded or ruin is built
        }
    }

    public void SetUpgradePrice(int price)
    {

        upgradePrice = price;
        if (townData.townUI.gameObject.activeSelf)
        {
            townData.townUI.Initialize(townData);//update the UI, only happens when town is upgraded or ruin is built
        }
    }
}
