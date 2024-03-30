using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathController
{

    private List<Vector3> road;
    private int currentIndex;

    public PathController()
    {
        road = new List<Vector3>();
        currentIndex = 0;
    }

    public void AddPathNode(Vector3 node)
    {
        road.Add(node);
    }

    public Vector3 GetNextNode()
    {
        return road[currentIndex++];
    }

    public bool EndOfTheRoad()
    {
        return currentIndex >= road.Count;
    }

}
