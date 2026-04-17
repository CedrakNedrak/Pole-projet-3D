using UnityEngine;

public class SpawnMiner : MonoBehaviour
{
    [SerializeField] private TownData townData;
    [SerializeField] private GameObject minerPrefab;

    [SerializeField] private int minerPrice = 50;
    public int MinerPrice => minerPrice;
    private Vector3 spawnPosition = new Vector3(0, 0, 0);

    public void Spawn()
    {
        Debug.Log("there!");
        if (townData.ressources.SpendGold(minerPrice))
        {
            Debug.Log("here!");
            Instantiate(minerPrefab, spawnPosition, minerPrefab.transform.rotation);
        }
    }

    public void SetMinerPrice(int price)
    {
        minerPrice = price;
    }
}
