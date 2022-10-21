using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.UI.Extensions;
public class BreadBoardManager : Singleton<BreadBoardManager>
{
    private Dictionary<Vector2, Vector2> linePointList = new Dictionary<Vector2, Vector2>();
    private MyBoard myboard;

    private List<Vector2> tempPointList = new List<Vector2>();

    [Header("板子上的素材")]
    [SerializeField] private DragItem operateRange;
    [SerializeField] private List<ElectricSlot> slotSet;
    [SerializeField] private Transform slotParent;
    [SerializeField] private GameObject myLine;
    [SerializeField] private Transform lineParent;
    private ElectricSlot firstSlot = null;


    [Header("線")]
    private UILineRenderer nowLine;
    private bool isOperate = false;
    [SerializeField] GameObject mouseFollower;

    [Header("顏色選擇")]
    [SerializeField] private List<Button> colorList;
    private float colorR;
    private float colorG;
    private float colorB;
    private float colorF = 255f;

    [Header("UI介面")]
    [SerializeField] private Button clearAllLineButton;
    private void Start()
    {
        myboard = new MyBoard();
        myboard.Init();
        InitSlot();
        ButtonInit();
    }

    [Header("電路查找")]
    [SerializeField] Button startFindButton;
    List<Node> nodeList;
    Queue<Vector2> pointAddList = new Queue<Vector2>();
    Vector2 myStart = new Vector2(1, 0);
    Vector2 myEnd = new Vector2(0, 0);
    Queue<Vector2> trashNodeList = new Queue<Vector2>();
    public AStarNode[,] nodes;
    List<AStarNode> closeList = new List<AStarNode>();
    List<AStarNode> openList = new List<AStarNode>();

    private void InitSlot()
    {
        operateRange.AddPointerDownListener(OnOperateRangePointerDown);
        operateRange.AddOnDragListener(OnOperateRangeOnDrag);
        operateRange.AddBeginDragListener(OnOperateRangeBeginDrag);
        operateRange.AddEndDragListener(OnOperateRangeEndDrag);

        nodes = new AStarNode[myboard.GetBoardRow(), myboard.GetBoardCol()];

        for (int i = 0; i < myboard.GetBoardRow(); i++)
        {
            for (int j = 0; j < myboard.GetBoardCol(); j++)
            {
                slotSet[i * myboard.GetBoardCol() + j].Init(i, j);
                AStarNode node = new AStarNode(i, j, Node_Type.Stop);
                nodes[i, j] = node;
            }
        }
    }

    private void ButtonInit()
    {
        for (int i = 0; i < colorList.Count; i++)
        {
            var index = i;
            colorList[i].onClick.AddListener(delegate { SelectColor(index); });
        }
        clearAllLineButton.onClick.AddListener(ClearAllLine);
        startFindButton.onClick.AddListener(delegate { StartFind(myStart, myEnd); });
    }

    /// <summary>
    /// 紅 黑 藍 綠 黃
    /// </summary>
    /// <param name="index"></param>
    private void SelectColor(int index)
    {
        switch (index)
        {
            case 0: colorR = 255f; colorG = 0f; colorB = 0f; colorF = 255f; break;
            case 1: colorR = 0f; colorG = 0f; colorB = 0f; colorF = 255f; break;
            case 2: colorR = 0f; colorG = 0f; colorB = 255f; colorF = 255f; break;
            case 3: colorR = 0f; colorG = 255f; colorB = 0f; colorF = 255f; break;
            case 4: colorR = 255f; colorG = 255f; colorB = 0f; colorF = 255f; break;
        }
    }

    /// <summary>
    /// 注意，這裡的now_x是指從上到下的row，
    /// now_y是指左到右的col
    /// </summary>
    /// <param name="now_x"></param>
    /// <param name="now_y"></param>
    /// <param name="electricType"></param>
    private void ChangeElectricMode(int now_x, int now_y, int electricType)
    {
        //如果該點的electrictype != 現在的electric type or -99 =>短路
        if (myboard.isElectric[now_x, now_y] != electricType ||
            myboard.isElectric[now_x, now_y] != -99)
        {

            //短路?
        }


        if (CheckIsHorizontalOrVertical(now_x))
        {
            for (int i = 0; i < myboard.GetBoardCol(); i++)
            {
                myboard.isElectric[now_x, i] = electricType;
            }
        }
        else
        {
            //將上排的值的給弄一弄
            if (now_x >= 2 && now_x <= 4)
            {
                for (int i = 2; i < 5; i++)
                {
                    myboard.isElectric[i, now_y] = electricType;
                }
            }
            else if (now_x >= 5 && now_x <= 7)
            {
                for (int i = 5; i < 8; i++)
                {
                    myboard.isElectric[i, now_y] = electricType;
                }
            }
        }
    }

    // true = Horizontal
    // false = Vertical
    private bool CheckIsHorizontalOrVertical(int nowRow)
    {
        if (nowRow <= 1 || nowRow >= 8) return true;
        else return false;
    }

    private void ClearAllLine()
    {
        for (int i = 0; i < lineParent.childCount; i++)
        {
            Destroy(lineParent.GetChild(i).gameObject.GetComponent<UILineRenderer>());
            Destroy(lineParent.GetChild(i).gameObject);
        }

        ResetAll();
    }

    private void ResetAll()
    {
        tempPointList.Clear();
        linePointList.Clear();
        nowLine = null;
        isOperate = false;
        myboard.ResetAll();
        for (int i = 0; i < slotSet.Count; i++)
        {
            slotSet[i].ResetSlot();
        }
        
        ///重製Node狀態
        for (int i = 0; i < myboard.GetBoardRow(); i++)
        {
            for (int j = 0; j < myboard.GetBoardCol(); j++)
            {
                nodes[i, j].type = Node_Type.Stop;
            }
        }
    }

    private void ClearNowLine(UILineRenderer uILineRenderer)
    {
        Destroy(uILineRenderer.gameObject);
    }

    private void OnOperateRangePointerDown(PointerEventData eventData)
    {
        if (eventData.pointerCurrentRaycast.gameObject != null)
        {
            if (eventData.pointerCurrentRaycast.gameObject.name.Contains("Normal"))
            {

                Vector2 point = eventData.position;
                Debug.Log("点的位置是" + point);
                nowLine = Instantiate(myLine, lineParent).GetComponent<UILineRenderer>();
                nowLine.color = new Vector4(colorR, colorG, colorB, colorF);
                //myLine.transform.parent = lineParent;
                //nowLine = myLine.GetComponent<UILineRenderer>();
                //nowLine.LineThickness = 10;
                //nowLine.rectTransform.anchoredPosition = new Vector2(0, 0);
                AddPointToLine(point);
                isOperate = true;

                firstSlot = eventData.pointerCurrentRaycast.gameObject.GetComponentInParent<ElectricSlot>();
            }
        }
    }

    private void OnOperateRangeBeginDrag(PointerEventData eventData)
    {

    }

    private void OnOperateRangeOnDrag(PointerEventData eventData)
    {
        if (!isOperate) return;
        //拖拉顯現需要setAllDirty 讓他髒 還要一直Dirty 所以要放在OnDrag
        nowLine.SetAllDirty();
        nowLine.Points[1] = eventData.position;
        mouseFollower.transform.position = eventData.position;
    }

    private void OnOperateRangeEndDrag(PointerEventData eventData)
    {
        if (!isOperate) return;
        if (eventData.pointerCurrentRaycast.gameObject != null)
        {
            if (eventData.pointerCurrentRaycast.gameObject.name.Contains("Hoover"))
            {

                nowLine.Points[1] = eventData.position;
                ElectricSlot nowSlot = eventData.pointerCurrentRaycast.gameObject.GetComponentInParent<ElectricSlot>();
                if (nowSlot == firstSlot)
                {
                    isOperate = false;
                    nowLine = null;
                    return;
                }

                nowSlot.ChangeToActive();
                firstSlot.ChangeToActive();
                Debug.Log("格子的row" + nowSlot);
                Debug.Log("現在的位置是：" + eventData.position);
                //nowSlot有 但是nowSlot.row沒有
                myboard.ChangeObjectInBoard(firstSlot.row, firstSlot.col);
                myboard.ChangeObjectInBoard(nowSlot.row, nowSlot.col);
                Debug.Log("之前的位置是" + firstSlot.row + "OOOO" + firstSlot.col);
                Debug.Log("現在的位置是" + nowSlot.row + "OOOO" + nowSlot.col);

                isOperate = false;
                nowLine = null;
                Vector2 slot1 = new Vector2(firstSlot.row, firstSlot.col);
                Vector2 slot2 = new Vector2(nowSlot.row, nowSlot.col);
                linePointList.Add(slot1, slot2);
                return;
            }
        }

        firstSlot = null;
        Destroy(nowLine.gameObject);
        nowLine = null;


    }

    private void AddPointToLine(Vector2 point)
    {
        // 前提是使用 using System的情況下使用，將陣列清0
        Array.Clear(nowLine.Points, 0, nowLine.Points.Length);
        tempPointList.Clear();
        tempPointList.Add(point);
        tempPointList.Add(point);
        nowLine.Points = tempPointList.ToArray();

    }


    private List<AStarNode> StartFind(Vector2 start, Vector2 end)
    {
        LoadLevel1();

        pointAddList.Clear();
        trashNodeList.Clear();
        //intAddList.Enqueue(start);

        AStarNode startNode = nodes[(int)start.x, (int)start.y];
        AStarNode endNode = nodes[(int)end.x, (int)end.y];

        closeList.Clear();
        openList.Clear();

        //把開始點放入CloseList裡面
        startNode.parent = null;
        startNode.f = 0;
        startNode.g = 0;
        startNode.h = 0;
        closeList.Add(startNode);

        //FindPointAndAddNode(start, end);

        ///遍歷nodes，添加他們的neighbor
        for (int i = 0; i < myboard.GetBoardRow(); i++)
        {
            for (int j = 0; j < myboard.GetBoardCol(); j++)
            {
                if (myboard.isObjectInBoard[i, j] == true)
                {
                    AddNeighBor(nodes[i, j]);
                }
            }
        }

        while (true)
        {
            foreach (var neighbor in startNode.neighborList)
            {
                FindNearlyNodeToOpenList(neighbor, neighbor.g, startNode, endNode);
            }
            

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
            startNode = openList[0];
            openList.RemoveAt(0);

            if (startNode == endNode)
            {
                List<AStarNode> path = new List<AStarNode>();
                path.Add(endNode);
                while (endNode.parent != null)
                {
                    path.Add(endNode.parent);
                    endNode = endNode.parent;
                }
                path.Reverse();

                return path;
            }
        }


        

    }

    private void FindNearlyNodeToOpenList(AStarNode nextNode, float g, AStarNode parent, AStarNode end)
    {

        if (nextNode == null || nextNode.type == Node_Type.Stop ||
            closeList.Contains(nextNode) || openList.Contains(nextNode)) return;



        //計算f值
        nextNode.parent = parent;

        //計算g 我離起點的距離 = 我父親離起點的距離+ 我離我父親的距離
        nextNode.g = parent.g + g;
        nextNode.h = 0; // Mathf.Abs(end.x - node.x) + Mathf.Abs(end.y - node.y);
        nextNode.f = nextNode.g + nextNode.h;

        openList.Add(nextNode);
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
    /// 幫現在的節點添加鄰居關係
    /// 共分為兩步驟、查找他的另外一點，以及他自己的一排
    /// </summary>
    /// <param name="nowNode"></param>
    private void AddNeighBor(AStarNode nowNode)
    {
        int now_row = (int)nowNode.x;
        int now_col = (int)nowNode.y;
        Vector2 nowPos = new Vector2(now_row, now_col);
        Vector2 errorVector = new Vector2(-99, -99);

        ///將另一個點算入他的鄰居中
        if (GetLineAnotherPoint(nowPos) != errorVector)
        {
            float neighborX = GetLineAnotherPoint(nowPos).x;
            float neighborY = GetLineAnotherPoint(nowPos).y;
            nowNode.AddNeighbor(nodes[(int)neighborX, (int)neighborY]);
        }

        ///先判斷他的屬性為何、並找尋同一層之所有siblings 
        if (CheckIsHorizontalOrVertical(now_row))
        {
            now_col = 0;
            for (int i = 0; i < myboard.GetBoardCol(); i++)
            {
                if (myboard.isObjectInBoard[now_row, now_col])
                {
                    Debug.Log("偵測點為：" + now_row.ToString() + now_col.ToString());
                    nodes[now_row, now_col].type = Node_Type.Walk;
                    nowNode.AddNeighbor(nodes[now_row, now_col]);
                }
                now_col++;
                if (now_col >= myboard.GetBoardCol())
                {
                    break;
                }

            }
        }
        else //麵包版中間的屬性取直的
        {
            Debug.Log("偵測直點為：" + now_row.ToString() + now_col.ToString());
            if (now_row >= 2 && now_row <= 4)
            {
                now_row = 2;
                for (int i = 0; i < 3; i++)
                {
                    if (myboard.isObjectInBoard[now_row, now_col])
                    {
                        nodes[now_row, now_col].type = Node_Type.Walk;
                        nowNode.AddNeighbor(nodes[now_row, now_col]);
                    }
                    now_row++;
                    if (now_row >= 5)
                    {
                        break;
                    }
                }
            }
            else
            {
                now_row = 5;
                for (int i = 0; i < 3; i++)
                {
                    if (myboard.isObjectInBoard[now_row, now_col])
                    {
                        nodes[now_row, now_col].type = Node_Type.Walk;
                        nowNode.AddNeighbor(nodes[now_row, now_col]);
                    }
                    now_row++;
                    if (now_row >= 5)
                    {
                        break;
                    }
                }
            }
        }
    }

    private Vector2 GetLineAnotherPoint(Vector2 nowPos)
    {
        foreach (var nowLine in linePointList)
        {
            //Debug.Log("字典key:" + nowLine.Key + "，字典Value" + nowLine.Value);
            //Debug.Log("找點的另外一位置" + nowPos);
            if (nowLine.Key == nowPos)
            {
                // Debug.Log("EEE");
                return nowLine.Value;
            }
            else if (nowLine.Value == nowPos)
            {
                //Debug.Log("FFF");
                return nowLine.Key;
            }

        }
        Vector2 vector2 = new Vector2(-99, -99);
        return vector2;
    }

    private void ElectricSuccess()
    {

    }

    private void ElectricFail()
    {

    }


    private void LoadLevel1()
    {
        nodes[4, 2].type = Node_Type.Walk;
        nodes[4, 2].g = 10;
        nodes[4, 3].type = Node_Type.Walk;
        nodes[4, 3].g = 10;

    }
}

