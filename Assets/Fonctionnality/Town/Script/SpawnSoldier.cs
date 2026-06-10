using UnityEngine;

public class SpawnSoldier : MonoBehaviour
{
    [SerializeField] private TownData townData;
    [SerializeField] private GameObject soldierPrefab;
    [SerializeField] private EntretienManager entretienManager;

    [SerializeField] private int soldierPrice = 50;
    public int SoldierPrice => soldierPrice;

    private Vector3 spawnPosition;

    [SerializeField] private float spawnDistance = 2f;

    public void Spawn()
    {
        if (townData.ressources.SpendGold(soldierPrice))
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
                * (Quaternion.Euler(-90, 0, 0) * Quaternion.Euler(0, 90, 0));

            GameObject soldier = Instantiate(
                soldierPrefab,
                spawnPosition,
                spawnRotation
            );

            UnitEntretien entretien = soldier.GetComponent<UnitEntretien>();

            if (entretienManager != null && entretien != null)
                entretienManager.RegisterUnit(entretien);
        }
    }

    public void SetSoldierPrice(int price)
    {
        soldierPrice = price;
    }
}