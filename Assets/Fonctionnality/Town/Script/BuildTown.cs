using UnityEngine;

public class BuildTown : MonoBehaviour
{
    private int buildPrice = 1000;
    public int BuildPrice => buildPrice;

    [SerializeField] private TownData townData;
    [SerializeField] private GameObject townPrefab;
    public void Build()
    {
        Debug.Log(townData.ressources.GetGold());
        if (townData.ressources.SpendGold(buildPrice))
        {
            Debug.Log("paid");
            //change skin
            //Instantiate(townPrefab, ruin.transform.position, ruin.transform.rotation);

            //Destroy(ruin);
            //Destroy(transform.parent.gameObject);
        }
    }

    public void SetBuildPrice(int price)
    {
        buildPrice = price;
    }
}

