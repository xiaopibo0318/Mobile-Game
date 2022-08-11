using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI.Extensions;
using UnityEngine.EventSystems;

public class DrawWoodLine : MonoBehaviour
{
    [Header("線")]
    UILineRenderer lr;
    public Transform[] points;
    float pos_x, pos_y;
    List<Vector2> pointlist = new List<Vector2>();

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
    private CutWoodUI cutwoodUI;


    private void Awake()
    {
        lr = GetComponent<UILineRenderer>();
        Insatnce = this;
        isInstantiate = false;
        isCircle = false;
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
            AddNewPoint();
           // cutwoodUI.DrawLine();
           
        }
        if (isInstantiate)
        {
            if (Goal.Instance.getInZone())
            {
                Debug.Log("成功繞圈");
                isCircle = true;
            }
        }
        

    }

    public void AddNewPoint()
    {
        var point = new Vector2(pos_x, pos_y);
        myCollider = gameObject.GetComponent<EdgeCollider2D>();
        if (pointlist.Count< 300)
        {
            if (pointlist.Count>3 && pointlist[pointlist.Count-1] == point)
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
        eventData.pressPosition = point;
        eventData.position = point;
        LineOnClick(eventData);
        //IsGoAway(eventData);



    }


    public void IsGoAway(PointerEventData _eventData)
    {
        if(_eventData.pointerCurrentRaycast.gameObject.name == "Path")
        {
            Debug.Log("不是Path");
        }
    }

    public void ClearLine()
    {
        pointlist.Clear();
        if (isInstantiate) Destroy(goalInMap);
        isInstantiate = false;
        isCircle = false;
    }

    //public void OnPointerUp(PointerEventData eventData)
    //{
    //    this.GetComponent<CanvasGroup>().blocksRaycasts = true;
    //}

    //public void OnPointerDown(PointerEventData eventData)
    //{
    //    this.GetComponent<CanvasGroup>().blocksRaycasts = false;
    //    if (eventData.pointerCurrentRaycast.gameObject.name == "Path")
    //        Debug.Log("Yes");
    //    Debug.Log(eventData.pointerCurrentRaycast.gameObject.name);
    //}

    private void LineClickDown()
    {

    }
    private void LineOnClick(PointerEventData eventData)
    {
        this.GetComponent<CanvasGroup>().blocksRaycasts = false;
        Debug.Log("1");
        Debug.Log(eventData.position);
        if (eventData.pointerCurrentRaycast.gameObject != null)
        {
            Debug.Log(3);
            Debug.Log(eventData.pointerCurrentRaycast.gameObject.name);
        }
           
           
        Debug.Log(eventData.pointerCurrentRaycast.screenPosition);
    }

    private void LineClickUp()
    {

    }


}
