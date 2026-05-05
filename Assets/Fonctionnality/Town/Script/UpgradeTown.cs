using TMPro;
using UnityEngine;

public class UpgradeTown : MonoBehaviour
{
    [SerializeField] TownUI townUI;
    public void Upgrade()
    {
        townUI.townData.TownLevelingSystem.UpgradeTown();
    }
}
