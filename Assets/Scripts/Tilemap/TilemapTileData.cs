using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class TilemapTileData : MonoBehaviour
{
    private Dictionary<Vector3Int, ITileData> tilemapData = new Dictionary<Vector3Int, ITileData>();

    public ITileData GetTileData(Vector3Int tilePosition)
    {
        if (tilemapData.ContainsKey(tilePosition))
            return tilemapData[tilePosition];
        else
            return null;
    }

    public void SetTileData(ITileData data, Vector3Int key)
    {
        if (tilemapData.ContainsKey(key))
            tilemapData[key] = data;
        else
            tilemapData.Add(key, data);
    }
}
