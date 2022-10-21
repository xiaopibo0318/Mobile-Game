using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Node_Type
{
    Walk,
    Stop
}

/// <summary>
/// A*的格子
/// </summary>
public class AStarNode
{
    public int x;
    public int y;
    //尋路消耗
    public float f;
    //與起點距離
    public float g;
    //與終點距離
    public float h;
    public AStarNode parent;
    public Node_Type type;

    public List<AStarNode> neighborList = new List<AStarNode>();

    public AStarNode(int x, int y, Node_Type type)
    {
        this.x = x;
        this.y = y;
        this.type = type;
    }

    public void ResetNode()
    {
        this.neighborList.Clear();
    }

    public void AddNeighbor(AStarNode node)
    {
        this.neighborList.Add(node);
    }

}
