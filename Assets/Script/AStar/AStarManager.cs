using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AStarManager
{
    private static AStarManager instance;
    public static AStarManager Instance
    {
        get
        {
            if (instance == null)
                instance = new AStarManager();
            return instance;
        }

    }



    public int mapW;
    public int mapH;

    public AStarNode[,] nodes;

    private List<AStarNode> openList = new List<AStarNode>();
    private List<AStarNode> closeList = new List<AStarNode>();

    /// <summary>
    /// 地圖初始化
    /// </summary>
    /// <param name="w"></param>
    /// <param name="h"></param>
    public void InitMapInfo(int w, int h)
    {
        this.mapH = h;
        this.mapW = w;
        nodes = new AStarNode[w, h];

        for (int i = 0; i < w; i++)
        {
            for (int j = 0; j < h; j++)
            {
                AStarNode node = new AStarNode(i, j, Random.Range(0, 100) > 30 ? Node_Type.Walk : Node_Type.Stop);
                nodes[i, j] = node;
            }
        }
    }

    /// <summary>
    /// 尋路方法
    /// </summary>
    /// <param name="startPos"></param>
    /// <param name="endPos"></param>
    /// <returns></returns>
    public List<AStarNode> FindPath(Vector2 startPos, Vector2 endPos)
    {
        //判斷是否在地圖範圍內
        if (startPos.x < 0 || startPos.x >= mapW ||
            startPos.y < 0 || startPos.y >= mapH ||
            endPos.x < 0 || endPos.x >= mapW ||
            endPos.y < 0 || endPos.y >= mapH) return null;

        //判斷是否為阻擋，若阻擋的話那就不用尋了
        AStarNode start = nodes[(int)startPos.x, (int)startPos.y];
        AStarNode end = nodes[(int)endPos.x, (int)endPos.y];

        if (start.type == Node_Type.Stop || end.type == Node_Type.Stop)
            return null;

        //清空、關閉、開啟列表 ： 避免上一次的數據影響這一次的尋路
        closeList.Clear();
        openList.Clear();

        //把開始點放入CloseList裡面
        start.parent = null;
        start.f = 0;
        start.g = 0;
        start.h = 0;
        closeList.Add(start);

        while (true)
        {
            //八方向的尋找 也就是說你要找鄰近的點
            FindNearlyNodeToOpenList(start.x - 1, start.y - 1, 1.4f, start, end);
            FindNearlyNodeToOpenList(start.x, start.y - 1, 1f, start, end);
            FindNearlyNodeToOpenList(start.x + 1, start.y - 1, 1.4f, start, end);
            FindNearlyNodeToOpenList(start.x - 1, start.y, 1f, start, end);
            FindNearlyNodeToOpenList(start.x + 1, start.y, 1f, start, end);
            FindNearlyNodeToOpenList(start.x - 1, start.y + 1, 1.4f, start, end);
            FindNearlyNodeToOpenList(start.x, start.y + 1, 1f, start, end);
            FindNearlyNodeToOpenList(start.x + 1, start.y + 1, 1.4f, start, end);

            if (openList.Count == 0)
            {
                Debug.Log("空、死路");
                return null;
            }

            //找尋路中消耗最小的點、要把亂的排序一下
            openList.Sort(SortOpenList);

            //放入關閉列表中，並從開啟列表移除
            closeList.Add(openList[0]);
            //找的點為新的起點
            start = openList[0];
            openList.RemoveAt(0);

            if (start == end)
            {
                List<AStarNode> path = new List<AStarNode>();
                path.Add(end);
                while (end.parent != null)
                {
                    path.Add(end.parent);
                    end = end.parent;
                }
                path.Reverse();

                return path;
            }
        }
    }

    private int SortOpenList(AStarNode a, AStarNode b)
    {
        if (a.f > b.f)
            return 1;
        else if (a.f == b.f)
            return 1;
        else
            return -1;
    }

    /// <summary>
    /// 將鄰近點加入OpenList
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    private void FindNearlyNodeToOpenList(int x, int y, float g, AStarNode parent, AStarNode end)
    {
        //判斷是否在地圖範圍內
        if (x < 0 || x >= mapW ||
            y < 0 || y >= mapH) return;

        AStarNode node = nodes[x, y];
        if (node == null || node.type == Node_Type.Stop ||
            closeList.Contains(node) ||
            openList.Contains(node)) return;



        //計算f值
        node.parent = parent;

        //計算g 我離起點的距離 = 我父親離起點的距離+ 我離我父親的距離
        node.g = parent.g + g;
        node.h = Mathf.Abs(end.x - node.x) + Mathf.Abs(end.y - node.y);
        node.f = node.g + node.h;

        openList.Add(node);
    }



}
