using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Tilemaps;

public class TilemapTileData : MonoBehaviour
{
    public Dictionary<Vector3Int, ITileData> tilemapData = new Dictionary<Vector3Int, ITileData>();
    [SerializeField] Tilemap targetTilemap;

    private void Start()
    {
        GenerateNewTileDataset();
    }

    public void GenerateNewTileDataset()
    {
        tilemapData = new Dictionary<Vector3Int, ITileData>();

        BoundsInt bounds = targetTilemap.cellBounds;
        for (int x = bounds.xMin; x < bounds.xMax; x++)
        {
            for (int y = bounds.yMin; y < bounds.yMax; y++)
            {
                Vector3Int pos = new Vector3Int(x, y, 0);
                if (targetTilemap.GetTile(pos) == null)
                {
                    continue;
                }
                tilemapData.Add(pos, new TileAStarData());
            }
        }
    }

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
