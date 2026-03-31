using UnityEngine;

public class BuildTownButton : MonoBehaviour
{
    [SerializeField] private Ressources ressources;
    [SerializeField] private GameObject town;
    [SerializeField] private GameObject ruin;

    private int buildPrice = 10;
    public void BuildTown()
    {
        Debug.Log(ressources.GetGold());
        if (ressources.SpendGold(buildPrice))
        {
            Debug.Log("paid");
            Vector3 position = new Vector3(0,0,0);

            Instantiate(town, position, town.transform.rotation);
            
            Destroy(transform.parent.gameObject);
            Destroy(ruin);
        }
    }
}
