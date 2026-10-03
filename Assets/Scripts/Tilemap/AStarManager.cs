using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.Tilemaps;

[RequireComponent(typeof(Tilemap))]
[RequireComponent(typeof(TilemapTileData))]
public class AStarManager : MonoBehaviour
{
    private TilemapTileData selfTileData;
    private Tilemap selfTilemap;
    [SerializeField] int maxSteps = 10000; // 10,000

    public static AStarManager instance;
    private void Awake()
    {
        instance = this;
        selfTileData = GetComponent<TilemapTileData>();
        selfTilemap = GetComponent<Tilemap>();
    }

    private float GetDistanceScore(TileAStarData tile, TileAStarData target, float offset = 0)
    {
        return (selfTilemap.CellToWorld(tile.selfPos) - selfTilemap.CellToWorld(target.selfPos)).magnitude + offset;
    }

    private void SetTileCost(TileAStarData tile, TileAStarData start, TileAStarData target, float offset = 0)
    {
        tile.gScore = GetDistanceScore(tile, start, offset);
        tile.hScore = GetDistanceScore(tile, target);
    }

    public List<Vector3> GetPath(Vector3 startPos, Vector3 targetPos)
    {
        return null;
        TileAStarData startTile = selfTileData.GetTileData(selfTilemap.WorldToCell(startPos));
        TileAStarData targetTile = selfTileData.GetTileData(selfTilemap.WorldToCell(targetPos));

        if (startTile == null || targetTile == null)
        {
            Debug.LogWarning("Failed to get start/target tile");
            Debug.Log(startPos);
            Debug.Log(startTile);
            Debug.Log(targetPos);
            Debug.Log(targetTile);
            Debug.Log("#####");
            return null;
        }

        List<TileAStarData> openTiles = new List<TileAStarData>();
        List<TileAStarData> closedTiles = new List<TileAStarData>();

        List<Vector3> finalPath = new List<Vector3>();

        bool foundPath = false;
        int currentStep = 0;
        TileAStarData currentTile;

        SetTileCost(startTile, startTile, targetTile);
        openTiles.Add(startTile);
        while (!foundPath && currentStep < maxSteps)
        {
            // Sort nodes
            openTiles = openTiles.OrderByDescending(t => t.FScore()).ToList();
            currentTile = openTiles[0];
            openTiles.RemoveAt(0);
            closedTiles.Add(currentTile);

            if (currentTile == targetTile)
            {
                foundPath = true;
            }

            for (int x = startTile.selfPos.x - 1; x <= startTile.selfPos.x + 1; x++)
            {
                for (int y = startTile.selfPos.y - 1; y <= startTile.selfPos.y + 1; y++)
                {
                    if (x == startTile.selfPos.x && y == startTile.selfPos.y)
                        continue;
                    TileAStarData neighborTile = selfTileData.GetTileData(new Vector3Int(x, y, 0));
                    if (neighborTile == null || closedTiles.Contains(neighborTile))
                        continue;

                    if (!openTiles.Contains(neighborTile) || neighborTile.gScore < GetDistanceScore(neighborTile, startTile, currentTile.gScore))
                    {
                        SetTileCost(neighborTile, startTile, targetTile, currentTile.gScore);
                        neighborTile.parentTile = currentTile;
                        if (!openTiles.Contains(neighborTile))
                            openTiles.Add(neighborTile);
                    }
                }
            }
            currentStep++;
        }

        if (currentStep < maxSteps)
        {
            Debug.LogWarning("FAILED TO FIND PATH");
            return null;
        }
        
        bool madePath = false;
        TileAStarData current = targetTile;
        while (!madePath)
        {
            finalPath.Add((Vector3)current.selfPos);
            if (current == startTile)
                madePath = true;
            if (current.parentTile == null)
            {
                Debug.LogWarning("ERROR in generating path");
                return null;
            }
            current = current.parentTile;
        }

        
        return finalPath;
    }

}
