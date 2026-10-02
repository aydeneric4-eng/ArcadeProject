using System.Collections.Generic;
using UnityEngine;

public class TilemapTileData : MonoBehaviour
{
    private Dictionary<Vector3Int, ITileData> tilemapData = new Dictionary<Vector3Int, ITileData>();
}
