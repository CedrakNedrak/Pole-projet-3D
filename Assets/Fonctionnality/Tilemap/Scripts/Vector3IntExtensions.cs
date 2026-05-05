using UnityEngine;

public static class Vector3IntExtensions
{
    private const float TILE_SIZE = 1f;
    public static Vector3 ToWorldScale(this Vector3Int tilemapVector)
    {
        return new Vector3(tilemapVector.x, tilemapVector.y, tilemapVector.z) * TILE_SIZE;
    }
}
