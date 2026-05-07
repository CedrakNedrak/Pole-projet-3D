using UnityEngine;

public class TownLevelingSystem : MonoBehaviour
{
    [SerializeField] private TownData townData;
    [SerializeField] private int upgradePrice = 5;
    public int UpgradePrice => upgradePrice;
    [SerializeField] GameObject ruinGameObject;
    [SerializeField] GameObject townLevelOneGameObject;
    [SerializeField] private int townLevel = 1;
    public int TownLevel => townLevel;

    public void UpgradeTown()
    {
        if (townData.ressources.SpendGold(upgradePrice))
        {
            SetTownLevel(townLevel + 1);

            ruinGameObject.SetActive(false); 
            townLevelOneGameObject.SetActive(false);

            switch (townLevel)
            {
                case (0): ruinGameObject.SetActive(true); break;
                case (1): townLevelOneGameObject.SetActive(true);  break;
                default : break;
            }
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
