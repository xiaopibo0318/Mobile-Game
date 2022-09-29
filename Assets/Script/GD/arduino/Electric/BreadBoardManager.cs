using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI.Extensions;
public class BreadBoardManager : Singleton<BreadBoardManager>
{

    private List<Vector2> exeList = new List<Vector2>();
    private MyBoard myboard;

    private List<Vector2> tempPointList = new List<Vector2>();

    [Header("板子上的素材")]
    [SerializeField] private DragItem operateRange;
    [SerializeField] private List<GameObject> slotSet;
    [SerializeField] private Transform slotParent;
    [SerializeField] private GameObject myLine;
    [SerializeField] private Transform lineParent;



    [Header("線")]
    private UILineRenderer nowLine;
    private bool isOperate = false;
    private void Start()
    {
        myboard = new MyBoard();
        myboard.Init();
        InitSlot();
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
                nowLine.LineThickness = 10;
                //nowLine.rectTransform.anchoredPosition = new Vector2(0, 0);
                AddPointToLine(point);
                isOperate = true;
            }
        }
    }

    private void OnOperateRangeBeginDrag(PointerEventData eventData)
    {

    }

    private void OnOperateRangeOnDrag(PointerEventData eventData)
    {
        if (!isOperate) return;
        nowLine.Points[1] = eventData.position;
    }

    private void OnOperateRangeEndDrag(PointerEventData eventData)
    {
        if (eventData.pointerCurrentRaycast.gameObject != null)
        {
            if (eventData.pointerCurrentRaycast.gameObject.name.Contains("Normal"))
            {
                nowLine.Points[1] = eventData.position;
                ElectricSlot nowSlot = eventData.pointerCurrentRaycast.gameObject.GetComponentInParent<ElectricSlot>();

                Debug.Log("格子的row" + nowSlot);
                Debug.Log("現在的位置是：" + eventData.position);
                //nowSlot有 但是nowSlot.row沒有
                myboard.ChangeObjectInBoard(nowSlot.row, nowSlot.col);
                isOperate = false;
                nowLine = null;
                
                return;
            }
        }
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
}
