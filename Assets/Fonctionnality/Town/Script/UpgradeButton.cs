using TMPro;
using UnityEngine;

public class UpgradeButton : MonoBehaviour
{
    [SerializeField] private Ressources ressources;
    [SerializeField] private TMP_Text townLevelText;
    private int upgradePrice = 5;

    public void UpgradeTown()
    {
        if (ressources.SpendGold(upgradePrice))
        {
            townLevelText.text = "Level 2";
        }
        //else: visual "not enough gold" indicator
    }

    
}
