using UnityEngine;

public class SpawnMiner : MonoBehaviour
{
    [SerializeField] private TownData townData;
    [SerializeField] private GameObject minerPrefab;
    private EntretienManager entretienManager;

    private void Start()
    {
        entretienManager = FindObjectOfType<EntretienManager>();
    }
    [SerializeField] private int minerPrice = 50;
    public int MinerPrice => minerPrice;

    private Vector3 spawnPosition;

    [SerializeField] private float spawnDistance = 10f;

    public void Spawn()
    {
        if (townData.ressources.SpendGold(minerPrice))
        {
            float randomAngle = Random.Range(-Mathf.PI, Mathf.PI);

            Vector2 spawnDirection2D = new Vector2(
                Mathf.Cos(randomAngle),
                Mathf.Sin(randomAngle)
            );

            Vector3 spawnDirection = new Vector3(
                spawnDirection2D.x,
                spawnDirection2D.y,
                0
            );

            spawnPosition = transform.position + spawnDirection * spawnDistance;

            Quaternion spawnRotation =
                Quaternion.AngleAxis(
                    Vector2.SignedAngle(Vector2.right, spawnDirection2D),
                    Vector3.forward
                )
                * (Quaternion.Euler(0, 0, -90) * Quaternion.Euler(180, 0, 0));

            GameObject miner = Instantiate(
                minerPrefab,
                spawnPosition,
                spawnRotation
            );

            UnitEntretien entretien = miner.GetComponent<UnitEntretien>();

            if (entretienManager != null && entretien != null)
                entretienManager.RegisterUnit(entretien);
        }
    }

    public void SetMinerPrice(int price)
    {
        minerPrice = price;
    }
}