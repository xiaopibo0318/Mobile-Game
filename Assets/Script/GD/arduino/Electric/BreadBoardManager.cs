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
    [SerializeField] private List<ElectricSlot> slotSetInBoard;
    [SerializeField] private List<ElectricSlot> slotSetInOther;
    [SerializeField] private Transform slotParent;
    [SerializeField] private GameObject myLine;
    [SerializeField] private Transform lineParent;
    private ElectricSlot firstSlot = null;

    [Header("背包素材")]
    [SerializeField] private List<Item> detectBagList = new List<Item>();
    [SerializeField] private Inventory myBag;

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
    [SerializeField] private Button goBackButton;
    private void Start()
    {
        myboard = new MyBoard();
        myboard.Init();
        InitSlot();
        ButtonInit();

        LoadLevel1();
    }

    [Header("電路查找")]
    [SerializeField] Button startFindButton;
    List<Node> nodeList;
    Queue<Vector2> pointAddList = new Queue<Vector2>();
    Vector2 myStart = new Vector2(22, 12);
    Vector2 myEnd = new Vector2(24, 12);
    Queue<Vector2> trashNodeList = new Queue<Vector2>();
    public AStarNode[,] nodes;
    public AStarNode[,] nodes2;
    List<AStarNode> closeList = new List<AStarNode>();
    List<AStarNode> openList = new List<AStarNode>();

    private void InitSlot()
    {
        operateRange.AddPointerDownListener(OnOperateRangePointerDown);
        operateRange.AddOnDragListener(OnOperateRangeOnDrag);
        operateRange.AddBeginDragListener(OnOperateRangeBeginDrag);
        operateRange.AddEndDragListener(OnOperateRangeEndDrag);

        nodes = new AStarNode[myboard.GetBoardRow(), myboard.GetBoardCol()];
        nodes2 = new AStarNode[slotSetInOther.Count, 13];

        for (int i = 0; i < myboard.GetBoardRow(); i++)
        {
            for (int j = 0; j < myboard.GetBoardCol(); j++)
            {
                slotSetInBoard[i * myboard.GetBoardCol() + j].Init(i, j);
                AStarNode node = new AStarNode(i, j, Node_Type.Stop);
                nodes[i, j] = node;
            }
        }
        for (int i = 0; i < slotSetInOther.Count; i++)
        {
            slotSetInOther[i].Init(i, 12);
            AStarNode node = new AStarNode(i, 12, Node_Type.Stop);
            nodes2[i, 12] = node;

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
        goBackButton.onClick.AddListener(delegate { CanvasManager.Instance.openCanvas("original"); });
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
        for (int i = 0; i < slotSetInBoard.Count; i++)
        {
            slotSetInBoard[i].ResetSlot();
        }
        for (int i = 0; i < slotSetInOther.Count; i++)
        {
            slotSetInOther[i].ResetSlot();
        }
        ///重製Node狀態
        for (int i = 0; i < myboard.GetBoardRow(); i++)
        {
            for (int j = 0; j < myboard.GetBoardCol(); j++)
            {
                nodes[i, j].type = Node_Type.Stop;
            }
        }
        for (int i = 0; i < slotSetInOther.Count; i++)
        {
            nodes2[i, 12].type = Node_Type.Stop;
        }
        LoadLevel1();
    }


    private void OnOperateRangePointerDown(PointerEventData eventData)
    {
        for (int i = 0; i < detectBagList.Count; i++)
        {
            if (!myBag.itemList.Contains(detectBagList[i]))
            {
                SiginalUI.Instance.SiginalText("你尚未擁有足夠電子元件");
            }
        }

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


                Debug.Log("之前的位置是" + firstSlot.row + "OOOO" + firstSlot.col);
                Debug.Log("現在的位置是" + nowSlot.row + "OOOO" + nowSlot.col);

                AStarNode a;
                AStarNode b;
                AStarNode temp1 = null;
                AStarNode temp2 = null;
                for (int i = 0; i < slotSetInOther.Count; i++)
                {
                    if (slotSetInOther[i] == nowSlot)
                    {
                        temp2 = nodes2[i, 12];
                    }
                    if (slotSetInOther[i] == firstSlot)
                    {
                        temp1 = nodes2[i, 12];
                    }
                }

                if (nowSlot.col < 11) b = nodes[nowSlot.row, nowSlot.col];
                else b = temp2;
                if (firstSlot.col < 11) a = nodes[firstSlot.row, firstSlot.col];
                else a = temp1;

                a.type = Node_Type.Walk;
                b.type = Node_Type.Walk;


                nowSlot.ChangeToActive();
                firstSlot.ChangeToActive();
                Debug.Log("格子的row" + nowSlot);
                Debug.Log("現在的位置是：" + eventData.position);
                //nowSlot有 但是nowSlot.row沒有
                myboard.ChangeObjectInBoard(firstSlot.row, firstSlot.col);
                myboard.ChangeObjectInBoard(nowSlot.row, nowSlot.col);


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


        pointAddList.Clear();
        trashNodeList.Clear();
        //intAddList.Enqueue(start);

        AStarNode startNode;
        AStarNode endNode;
        if (start.y < 11)
        {
            startNode = nodes[(int)start.x, (int)start.y];
        }
        else
        {
            startNode = nodes2[(int)start.x, (int)start.y];
        }
        if (end.y < 11)
        {
            endNode = nodes[(int)end.x, (int)end.y];
        }
        else
        {
            endNode = nodes2[(int)end.x, (int)end.y];
        }

        closeList.Clear();
        openList.Clear();

        //把開始點放入CloseList裡面
        startNode.parent = null;
        startNode.f = 0;
        startNode.g = 0;
        startNode.h = 0;
        closeList.Add(startNode);

        //FindPointAndAddNode(start, end);

        LoadLevel1();

        ///遍歷nodes，添加他們的neighbor
        for (int i = 0; i < myboard.GetBoardRow(); i++)
        {
            for (int j = 0; j < myboard.GetBoardCol(); j++)
            {
                if (myboard.isObjectInBoard[i, j] == true || nodes[i, j].type == Node_Type.Walk)
                {
                    AddNeighBor(nodes[i, j]);
                }
            }
        }
        for (int i = 0; i < slotSetInOther.Count; i++)
        {
            if (nodes2[i, 12].type == Node_Type.Walk)
            {
                AddNeighBor(nodes2[i, 12]);
            }
        }

        while (true)
        {
            foreach (var neighbor in startNode.neighborList)
            {
                Debug.Log("現在的點的x:" + startNode.x + ",y:" + startNode.y);
                Debug.Log("現在的點的鄰居x:" + neighbor.x + ",y:" + neighbor.y);
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
                float totalcost = 0;

                List<AStarNode> path = new List<AStarNode>();
                path.Add(endNode);
                float temp = 0;
                while (endNode.parent != null)
                {
                    path.Add(endNode.parent);
                    if (temp < endNode.g)
                    {
                        temp = endNode.g;
                    }
                    endNode = endNode.parent;
                }
                totalcost = temp;
                path.Reverse();

                for (int i = 0; i < path.Count; i++)
                {
                    Debug.Log("路徑為：" + path[i].x + "," + path[i].y);
                }
                Debug.Log("總消耗為：" + totalcost);
                if (totalcost != 10) ElectricFail();
                else ElectricSuccess();

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
            if ((int)neighborY < 11) nowNode.AddNeighbor(nodes[(int)neighborX, (int)neighborY]);
            else nowNode.AddNeighbor(nodes2[(int)neighborX, (int)neighborY]);
        }


        if (now_col > 11) return;
        ///先判斷他的屬性為何、並找尋同一層之所有siblings 
        if (CheckIsHorizontalOrVertical(now_row))
        {
            now_col = 0;
            for (int i = 0; i < myboard.GetBoardCol(); i++)
            {
                //if (now_col == (int)nowNode.y) continue;

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
                    //if (now_row == (int)nowNode.x) continue;

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
                    //if (now_row == (int)nowNode.x) continue;

                    if (myboard.isObjectInBoard[now_row, now_col])
                    {
                        nodes[now_row, now_col].type = Node_Type.Walk;
                        nowNode.AddNeighbor(nodes[now_row, now_col]);
                    }
                    now_row++;
                    if (now_row >= 8)
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
        SiginalUI.Instance.SiginalText("成功");
        GDMananger.Instance.gameStatus = 4;
        GDMananger.Instance.UpdateMap();
    }

    private void ElectricFail()
    {
        SiginalUI.Instance.SiginalText("失敗");
    }


    private void LoadLevel1()
    {
        nodes[4, 2].type = Node_Type.Walk;
        nodes[4, 2].g = 5;
        nodes[3, 3].type = Node_Type.Walk;
        nodes[3, 3].g = 5;
        nodes[2, 3].type = Node_Type.Walk;
        nodes[2, 4].type = Node_Type.Walk;

        ///建立節點關係
        nodes[4, 2].AddNeighbor(nodes[3, 3]);
        nodes[3, 3].AddNeighbor(nodes[4, 2]);
        nodes[2, 3].AddNeighbor(nodes[2, 4]);
        nodes[2, 4].AddNeighbor(nodes[2, 3]);


        ElectricSlot slot1 = slotSetInBoard[4 * myboard.GetBoardCol() + 2];
        ElectricSlot slot2 = slotSetInBoard[3 * myboard.GetBoardCol() + 3];
        ElectricSlot slot3 = slotSetInBoard[2 * myboard.GetBoardCol() + 3];
        ElectricSlot slot4 = slotSetInBoard[2 * myboard.GetBoardCol() + 4];
        slot1.ChangeToActive();
        slot2.ChangeToActive();
        slot3.ChangeToActive();
        slot4.ChangeToActive();
        myboard.isObjectInBoard[4, 2] = true;
        myboard.isObjectInBoard[3, 3] = true;
        myboard.isObjectInBoard[2, 3] = true;
        myboard.isObjectInBoard[2, 4] = true;




    }
}

