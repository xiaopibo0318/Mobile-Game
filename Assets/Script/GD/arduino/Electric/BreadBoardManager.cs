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
    [SerializeField] private List<GameObject> slotSet;
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

    private void Start()
    {
        myboard = new MyBoard();
        myboard.Init();
        InitSlot();
        ColorButtonInit();
    }


    private void InitSlot()
    {
        operateRange.AddPointerDownListener(OnOperateRangePointerDown);
        operateRange.AddOnDragListener(OnOperateRangeOnDrag);
        operateRange.AddBeginDragListener(OnOperateRangeBeginDrag);
        operateRange.AddEndDragListener(OnOperateRangeEndDrag);

        for (int i = 0; i < myboard.GetBoardRow(); i++)
        {
            for (int j = 0; j < myboard.GetBoardCol(); j++)
            {
                slotSet[i].GetComponent<ElectricSlot>().Init(i, j);
            }
        }
    }

    private void ColorButtonInit()
    {
        for (int i = 0; i < colorList.Count; i++)
        {
            var index = i;
            colorList[i].onClick.AddListener(delegate { SelectColor(index); });
        }
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

    private void StartDetect()
    {
        foreach (var item in myboard.isObjectInBoard)
        {

        }
    }

    private void ClearAllLine()
    {
        foreach (var myLine in lineParent.GetComponentsInChildren<GameObject>())
        {

            Destroy(myLine.gameObject);
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
                myboard.ChangeObjectInBoard(nowSlot.row, nowSlot.col);
                isOperate = false;
                nowLine = null;

                return;
            }
        }
        firstSlot = null;
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

    private void StartFind(Vector2 start, Vector2 end)
    {
        FindNextPoint(start, end);
    }

    /// <summary>
    /// 開始遞迴尋找 ，但還要考慮 上下左右的分開尋找(尚未處理)
    /// </summary>
    /// <param name="nowPos"></param>
    private void FindNextPoint(Vector2 nowPos, Vector2 end)
    {
        int now_row = (int)nowPos.x;
        int now_col = (int)nowPos.y;
        //先找尋
        if (CheckIsHorizontalOrVertical(now_row))
        {
            if (!myboard.isObjectInBoard[now_row, now_col])
            {
                Vector2 nextPos = new Vector2(now_row, now_col + 1);
                if (now_col > myboard.GetBoardCol())
                    ElectricFail();
                else
                    FindNextPoint(nextPos, end);
            }
            else //找尋該點的下一個節點為何
            {
                Vector2 nextPos = GetLineAnotherPoint(nowPos);
                if (nextPos == end)
                    ElectricSuccess();
                else
                    FindNextPoint(GetLineAnotherPoint(nextPos), end);
            }
        }
        else
        {

        }
    }

    private Vector2 GetLineAnotherPoint(Vector2 nowPos)
    {

        foreach (var nowLine in linePointList)
        {
            if (nowLine.Key == nowPos) return nowLine.Value;
            else if (nowLine.Value == nowPos) return nowLine.Key;
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



}

