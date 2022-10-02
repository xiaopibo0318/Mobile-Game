using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node 
{
    public Vector2 position; //(row,col)
    public Node parent;
    public bool isObstacle;


    public Node()
    {
        this.isObstacle = false;
        this.parent = null;
    }

    public Node(Vector2 pos)
    {
        this.isObstacle = false;
        this.parent = null;
        this.position = pos;
    }

    public void MakeObstacle()
    {
        this.isObstacle = true;
    }

    
}
