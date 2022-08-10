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

    private void Awake()
    {
        lr = GetComponent<UILineRenderer>();
        Insatnce = this;
        isInstantiate = false;
        isCircle = false;
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
        if (Input.GetMouseButton(1))
        {
            
            AddNewPoint();
            
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


    }

    public void ClearLine()
    {
        pointlist.Clear();
        if (isInstantiate) Destroy(goalInMap);
        isInstantiate = false;
        isCircle = false;
    }

}
