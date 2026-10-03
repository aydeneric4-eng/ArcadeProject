using UnityEngine;

public class TileAStarData : ITileData
{
    public AStarNode parentNode;
    public float gScore;
    public float hScore;

    public float FScore()
    {
        return gScore + hScore;
    }
}
