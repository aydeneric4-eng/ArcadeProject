using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;

public class AStarNode : MonoBehaviour
{
    // https://www.youtube.com/watch?v=UHnOW-OimLQ

    // https://www.youtube.com/watch?v=HCt_CYOW9jg
    // https://www.youtube.com/watch?v=YGlaAwriMx0&list=PLX-uZVK_0K_6GjJ_tgg1YXmO8lor7sRM7&index=5
    public AStarNode cameFrom;
    public List<AStarNode> connections;

    public float gScore;
    public float hScore;

    public float FScore()
    {
        return gScore + hScore;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;

        if (connections.Count > 0)
        {
            for (int i = 0; i < connections.Count; i++)
            {
                Gizmos.DrawLine(transform.position, connections[i].transform.position);
            }
        }
    }
}
