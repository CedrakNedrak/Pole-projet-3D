using TMPro;
using UnityEngine;

public class ResourceUI : MonoBehaviour
{
    [SerializeField] private TMP_Text resourceText;
    [SerializeField] private Ressources ressources;
    private int gold = 0;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    // Update is called once per frame
    void Update()
    {
        gold = ressources.GetGold();
        resourceText.text = "Gold: "+gold.ToString();
    }
}
