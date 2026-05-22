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
            float randomAngle = Random.Range(0f, 2 * Mathf.PI);
            Vector2 spawnDirection2D = new Vector2(Mathf.Cos(randomAngle), Mathf.Sin(randomAngle));
            Vector3 spawnDirection = new Vector3(spawnDirection2D.x, spawnDirection2D.y, 0);
            spawnPosition = transform.position + spawnDirection * spawnDistance;

            float angle = Vector2.SignedAngle(Vector2.right, spawnDirection2D);
            Quaternion firstRotation = Quaternion.Euler(0, 0, -90 + angle);//order of rotation: z, x, y
            Quaternion secondRotation = Quaternion.Euler(-90, 0, 0);
            Quaternion finalRot = firstRotation * secondRotation;

            Instantiate(minerPrefab, spawnPosition, finalRot);
        }
    }

    public void SetMinerPrice(int price)
    {
        minerPrice = price;
    }
}
