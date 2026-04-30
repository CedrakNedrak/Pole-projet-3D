using UnityEngine;

public class SpawnMiner : MonoBehaviour
{
    [SerializeField] private TownData townData;
    [SerializeField] private GameObject minerPrefab;

    [SerializeField] private int minerPrice = 50;
    public int MinerPrice => minerPrice;
    private Vector3 spawnPosition;
    [SerializeField] private float spawnDistance = 2f;

    public void Spawn()
    {
        if (townData.ressources.SpendGold(minerPrice))
        {
            Vector2 spawnDirection2D = Random.onUnitSphere;
            Vector3 spawnDirection = new Vector3(spawnDirection2D.x, spawnDirection2D.y, 0);
            spawnPosition = transform.position + spawnDirection * spawnDistance;
            Debug.Log(Vector2.SignedAngle(Vector2.right, spawnDirection2D));
            Quaternion spawnRotation = Quaternion.Euler(0, 180, -Vector2.SignedAngle(Vector2.right, spawnDirection2D));//180 because of prefab
            Instantiate(minerPrefab, spawnPosition, spawnRotation);
        }
    }

    public void SetMinerPrice(int price)
    {
        minerPrice = price;
    }
}
