using UnityEngine;

[System.Serializable]
public class DiscoverableData
{
    public Vector3Int cell;
    public GameObject prefab;

    public Vector3 position;
    public Quaternion rotation;
    public Vector3 scale;

    public GameObject instance;
    public bool discovered;
}