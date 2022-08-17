using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI.Extensions;
using UnityEngine.EventSystems;
using System;

public class DrawWoodLine : MonoBehaviour
{
    [Header("線")]
    UILineRenderer lr;
    public Transform[] points;
    float pos_x, pos_y;
    List<Vector2> pointlist = new List<Vector2>();
    bool startDraw;

    [Header("邊界判定")]
    EdgeCollider2D myCollider;

    [Header("完成死圈判定")]
    public GameObject goal;
    bool isInstantiate;
    public GameObject goalInMap;
    bool isCircle;

    [Header("動畫")]
    public RectTransform brokenWood;

    public static DrawWoodLine Insatnce;

    [Header("事件判定")]
    public EventSystem eventSystem;
    public GameObject myPos;
    private bool isOperate = false;

    [Header("計時")]
    bool timeStart;

    CutWoodManager cutWoodManager;
    DragImageWood dragImageWood;

    private void Awake()
    {
        lr = GetComponent<UILineRenderer>();
        Insatnce = this;
        isInstantiate = false;
        isCircle = false;
        startDraw = false;
        timeStart = false;
        cutWoodManager = GameObject.Find("CuttingWood").GetComponent<CutWoodManager>();
        //siginalUI = GameObject.FindGameObjectWithTag("SignalUI").GetComponent<SiginalUI>();
        dragImageWood = GameObject.Find("CuttingWood").GetComponent<DragImageWood>();
        ///<summary>
        ///添加 "點擊、拖曳" 的處理
        /// </summary>

        dragImageWood.AddPointerDownListener(OnUserPointerDown);
        dragImageWood.AddOnDragListener(OnOperateRangeDrag);
        dragImageWood.AddBeginDragListener(OnOperateRangeBeginDrag);
        dragImageWood.AddEndDragListener(OnOperateRangeEndDrag);

        ClearLine();
        //cutwoodUI.AddOperateLineListener(LineClickDown,LineOnClick,LineClickUp);
    }

 

    public void SetUpLine(Transform[] points)
    {

        //lr.positionCount = points.Length;
        this.points = points;
        
        
    }


    public void FixedUpdate()
    {
        pos_x = Input.mousePosition.x-900;
        pos_y = Input.mousePosition.y-450;
        //Debug.Log(TouchDetect());

    }

    public void Update()
    {
        
        float a = 10;
        while (a > 0)
        {
            a -= Time.deltaTime;
        }
        if (Input.GetMouseButton(0) || (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved))
        {
            startDraw = true;
            AddNewPoint();
           // cutwoodUI.DrawLine();
           
        }
        if (isInstantiate)
        {
            if (Goal.Instance.getInZone())
            {
                Debug.Log("成功繞圈");
                isCircle = true;
                cutWoodManager.CutSucced();
            }
        }
        

    }

    public void AddNewPoint()
    {
        if (!timeStart)
        {
            //開始計時
            cutWoodManager.StartCutWood();
            timeStart = true;
        }
        var point = new Vector2(pos_x, pos_y);
        myCollider = gameObject.GetComponent<EdgeCollider2D>();
        if (!cutWoodManager.getTimeStatus())
        {
            if (pointlist.Count < 3)
            {
                myCollider.points[0] = point;
                myCollider.points[1] = point;
            }
            
            if (pointlist.Count > 3 && pointlist[pointlist.Count - 1] == point)
            {
                return;
            }
            pointlist.Add(point);
            lr.Points = pointlist.ToArray();
            //防止起點碰到Goal
            //if (pointlist.Count < 15)
            //{
            //    return;
            //}
            myCollider.points = lr.Points;
        }
        else timeStart = false;

        ///<summary>
        ///繞完一圈的判定
        /// </summary>
        if (pointlist.Count > 75 && !isInstantiate)
        {
            var pos = new Vector2(pointlist[0].x + 900 - 15, pointlist[0].y + 450 - 15 );
            goalInMap = Instantiate(goal,pos , Quaternion.Euler(0,0,0), this.transform.parent);
            isInstantiate = true;
        }
        PointerEventData eventData = new PointerEventData(eventSystem);




    }


    public void CutFail()
    {
        cutWoodManager.CutFail();
        ClearLine();
    }


    public void ClearLine()
    {
        pointlist.Clear();
        if (isInstantiate) Destroy(goalInMap);
        isInstantiate = false;
        isCircle = false;
    }

    public bool TouchDetect()
    {
        bool isTouch = EventSystem.current.IsPointerOverGameObject();
        return isTouch;
    }


    private void OnUserPointerDown(PointerEventData eventData)
    { 
        if (cutWoodManager.getTimeStatus())
        {
            cutWoodManager.StartCutWood();
            isOperate = true;
        }
        else
        {
            isOperate = false;
            return;
        }

        
        Debug.Log("pointer down is touch.");
        if (eventData.pointerCurrentRaycast.gameObject != null)
        {
            Debug.Log(eventData.pointerCurrentRaycast.gameObject.name);
        }
        else Debug.Log("沒東西");

    }

    private void OnOperateRangeBeginDrag(PointerEventData eventData)
    {
        if (!isOperate) return;
        if (cutWoodManager.getTimeStatus()){
            CutFail();
            myPos.GetComponent<CanvasGroup>().blocksRaycasts = true;
        }
        myPos.GetComponent<CanvasGroup>().blocksRaycasts = false;
    }

    private void OnOperateRangeDrag(PointerEventData eventData)
    {
        if (!isOperate) return;

        if (eventData.pointerCurrentRaycast.gameObject != null)
        {
            if(eventData.pointerCurrentRaycast.gameObject.name != "Path")
            {
                CutFail();
            }
        }
        else Debug.Log("沒東西");
    }

    private void OnOperateRangeEndDrag(PointerEventData eventData)
    {
        isOperate = false;


        if (isInstantiate)
        {
            if (Goal.Instance.getInZone())
            {
                Debug.Log("成功繞圈");
                isCircle = true;
                cutWoodManager.CutSucced();
                ClearLine();
            }
        }
        else
        {
            CutFail();
        }
        myPos.GetComponent<CanvasGroup>().blocksRaycasts = true;
    }


}
