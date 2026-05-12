using System.Collections.Generic;
using UnityEngine;

public class DiscoveryManager : MonoBehaviour
{
    private Dictionary<Vector3Int, List<DiscoverableData>> objectsByCell =
        new Dictionary<Vector3Int, List<DiscoverableData>>();

    public void Register(Vector3Int cell, GameObject prefab)
    {
        Register(
            cell,
            prefab,
            new Vector3(cell.x, cell.y, 0),
            Quaternion.identity,
            prefab.transform.localScale
        );
    }

    public void Register(Vector3Int cell, GameObject prefab, Quaternion rotation)
    {
        Register(
            cell,
            prefab,
            new Vector3(cell.x, cell.y, 0),
            rotation,
            prefab.transform.localScale
        );
    }

    public void Register(
        Vector3Int cell,
        GameObject prefab,
        Vector3 position,
        Quaternion rotation,
        Vector3 scale
    )
    {
        DiscoverableData data = new DiscoverableData
        {
            cell = cell,
            prefab = prefab,
            position = position,
            rotation = rotation,
            scale = scale,
            instance = null,
            discovered = false
        };

        if (!objectsByCell.ContainsKey(cell))
            objectsByCell[cell] = new List<DiscoverableData>();

        objectsByCell[cell].Add(data);
    }

    public void RevealAt(Vector3Int cell)
    {
        if (!objectsByCell.ContainsKey(cell))
            return;

        foreach (DiscoverableData data in objectsByCell[cell])
        {
            if (data.discovered)
                continue;

            GameObject obj = Instantiate(data.prefab, data.position, data.rotation);
            obj.transform.localScale = data.scale;

            data.instance = obj;
            data.discovered = true;
        }
    }
}