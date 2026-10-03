using UnityEngine;

public class TileAStarData : ITileData
{
    public Vector3Int selfPos;
    public TileAStarData parentTile;
    public float gScore;
    public float hScore;

    public float FScore()
    {
        return gScore + hScore;
    }
}
