using UnityEngine;

public class TownGenerateGold : MonoBehaviour
{
    [Header("Production")]
    [SerializeField] private int startGold = 200;
    public int StartGold => startGold;
    [SerializeField] private int goldIncome = 70;
    public int GoldIncome => goldIncome;

    [Header("Références")]
    [SerializeField] private TownData townData;

    private float timer;

    private void Start()
    {
        SetGoldIncome(goldIncome);
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= 1f)
        {
            GenerateGold();
            timer = 0f;
        }
    }

    void GenerateGold()
    {
        if (townData.ressources != null && townData.TownLevelingSystem.TownLevel > 0)
        {
            townData.ressources.AddGold(goldIncome);
        }
    }

    public void SetGoldIncome(int goldIncome)
    {
        this.goldIncome = goldIncome;
    }

    public void SetStartGold(int startGold)
    {
        this.startGold = startGold;
        townData.ressources.SetGold(startGold);
    }
}