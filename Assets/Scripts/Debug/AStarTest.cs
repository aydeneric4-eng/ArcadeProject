using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;
using TMPro;

public class AStarTest : MonoBehaviour
{
    [SerializeField] AStarManager manager;
    [SerializeField] Transform startTransform;
    [SerializeField] Transform targetTransform;

    [SerializeField] private List<Vector3> path = new List<Vector3>();

    private int count = 50;
    [SerializeField] int framesPerRequest = 50;
    private bool fire = true;
    private void FixedUpdate()
    {
        if (!(count < framesPerRequest) && fire)
        {
            fire = false;
            RequestAStarPath();
            count = 0;
        }
        count++;
    }

    public void RequestAStarPath()
    {
        //Debug.Log(targetTransform);
        //Debug.Log(targetTransform.position);
        path = manager.GetPath(startTransform.position, targetTransform.position);
        if (path == null)
        {
            Debug.Log("GOT NULL PATH");
        }
        Debug.Log("path:");
        Debug.Log(path);
    }

    private void OnDrawGizmos()
    {
        if (path == null)
            return;
        if (path.Count < 2)
            return;

        Gizmos.color = Color.red;

        for (int i = 1; i <= path.Count; i++)
        {
            Debug.Log(i);
            //Gizmos.DrawLine(path[i - 1], path[i]);
        }
    }
}
