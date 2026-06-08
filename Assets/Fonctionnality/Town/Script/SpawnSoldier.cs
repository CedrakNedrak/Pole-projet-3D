using UnityEngine;

public class SpawnSoldier : MonoBehaviour
{
    [SerializeField] private TownData townData;
    [SerializeField] private GameObject soldierPrefab;

    [SerializeField] private int soldierPrice = 50;
    public int SoldierPrice => soldierPrice;
    private Vector3 spawnPosition;
    [SerializeField] private float spawnDistance = 2f;

    public void Spawn()
    {
        if (townData.ressources.SpendGold(soldierPrice))
        {
            Vector2 spawnDirection2D = Random.onUnitSphere;
            Vector3 spawnDirection = new Vector3(spawnDirection2D.x, spawnDirection2D.y, 0);
            spawnPosition = transform.position + spawnDirection * spawnDistance;
            Debug.Log(Vector2.SignedAngle(Vector2.right, spawnDirection2D));
            Quaternion spawnRotation = Quaternion.Euler(180, 0, -Vector2.SignedAngle(Vector2.right, spawnDirection2D));//180 because of prefab
            Instantiate(soldierPrefab, spawnPosition, spawnRotation);
        }
    }

    public void SetSoldierPrice(int price)
    {
        soldierPrice = price;
    }
}
