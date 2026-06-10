using System.Collections.Generic;
using UnityEngine;

public class OreManager : MonoBehaviour
{
    [SerializeField] private int goldPerOre = 25;

    private Dictionary<Vector3Int, GameObject> oreObjectsByCell =
        new Dictionary<Vector3Int, GameObject>();

    public void RegisterOre(Vector3Int cell, GameObject oreObject)
    {
        if (!oreObjectsByCell.ContainsKey(cell))
            oreObjectsByCell.Add(cell, oreObject);
    }

    public void TryMineOre(Vector3Int cell)
    {
        if (!oreObjectsByCell.ContainsKey(cell))
            return;

        GameObject oreObject = oreObjectsByCell[cell];

        if (oreObject != null)
            Destroy(oreObject);

        oreObjectsByCell.Remove(cell);

        Ressources ressources = FindObjectOfType<Ressources>();

        if (ressources != null)
            ressources.AddGold(goldPerOre);

        Debug.Log("[OreManager] Minerai miné : +" + goldPerOre + " or");
    }
}