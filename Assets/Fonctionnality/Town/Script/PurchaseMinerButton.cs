using Unity.Mathematics;
using UnityEngine;

public class PurchaseMinerButton : MonoBehaviour
{
    [SerializeField] private Ressources ressources;
    [SerializeField] private GameObject miner;
 
    
    private int minerPrice = 10;
    private Vector3 spawnPosition = new Vector3(0,0,0);


    public void PurchaseMiner()
    {
        if (ressources.SpendGold(minerPrice))
        {
            Instantiate(miner,spawnPosition, miner.transform.rotation);
        }
        //else: visual "not enough Gold" indicator
    }
}
