using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI.Extensions;

public class DrawLine2Main : MonoBehaviour
{
    [SerializeField] private DragItem operateRange;
    [SerializeField] private RectTransform mouseFollower;
    [SerializeField] private UILineRenderer finishedLine;
    [SerializeField] private UILineRenderer previewLine;
    [SerializeField] private GameObject pointOnPath;
    [SerializeField] private RectTransform wood301Trans;
    private Vector2[] targetPoints;
    private List<Vector2> finishedPoints;
    private Vector2[] previewPoints;

    private int mouseFollowerSize = 10;

    private bool isOperate = false;
    private bool isFinished = false;
    private int startIndex;
    private int currentIndex;
    private int nextIndex;
    private int nextValue;  //計算下一個點是往前還是往後

    private void Start()
    {
        targetPoints = new Vector2[8]
        {
            new Vector2(360,360),
            new Vector2(250,360),
            new Vector2(140,360),
            new Vector2(140,250),
            new Vector2(140,140),
            new Vector2(250,140),
            new Vector2(360,140),
            new Vector2(360,250) 
        };

        finishedPoints = new List<Vector2>();
        previewPoints = new Vector2[2];
        previewLine.Points = previewPoints;

        mouseFollower.sizeDelta = new Vector2(mouseFollowerSize * 2, mouseFollowerSize * 2);

        operateRange.AddPointerDownListener(OnOperateRangePointerDown);
        operateRange.AddOnDragListener(OnOperateRangeDrag);
        operateRange.AddBeginDragListener(OnOperateRangeBeginDrag);
        operateRange.AddEndDragListener(OnOperateRangeEndDrag);

        CreatePointOnPath();
    }

    private void CreatePointOnPath()
    {
        for(int i = 0; i < targetPoints.Length; i++)
        {
            Vector3 nowPos = new Vector3 (targetPoints[i].x+900, targetPoints[i].y+450, 0);
            Instantiate(pointOnPath, nowPos,Quaternion.identity,wood301Trans);
        }
    }


    private void OnOperateRangePointerDown(PointerEventData eventData)
    {
        Debug.Log("有案到下去");
        bool isTouch = false;
        for (int i = 0; i < targetPoints.Length; i++)
        {
            Vector2 myPoint = new Vector2(targetPoints[i].x + 900, targetPoints[i].y + 450);
            if (IsTouch(eventData.position, myPoint, mouseFollowerSize))
            {
                startIndex = i;
                isTouch = true;
                break;
            }
        }

        if (!isTouch)
            return;

        Debug.Log("pointer down is touch.");
        isOperate = true;
        currentIndex = startIndex;
        nextIndex = -99;
    }

    private void OnOperateRangeBeginDrag(PointerEventData eventData)
    {
        Debug.Log("有開始脫");
        if (!isOperate)
            return;

        Vector2 point = targetPoints[currentIndex];
        finishedPoints.Add(point);
        finishedLine.Points = finishedPoints.ToArray();
        finishedLine.gameObject.SetActive(true);
        previewPoints[0] = point;
        previewPoints[1] = point;
        previewLine.gameObject.SetActive(true);
        isFinished = false;
        nextIndex = -99;
        nextValue = 0;
    }

    private void OnOperateRangeDrag(PointerEventData eventData)
    {
        if (!isOperate)
            return;

        //設置mouseFollower的位置
        mouseFollower.position = eventData.position;
        previewPoints[1] = eventData.position;
        previewLine.SetAllDirty();

        //若已完成，則return
        if (isFinished)
            return;

        //若為無效值，判斷跟哪個鄰近點比較接近
        if (nextIndex == -99)
        {
            int tempIndex = CycleValue(currentIndex + 1, targetPoints.Length - 1);
            if (IsTouch(eventData.position, targetPoints[tempIndex], mouseFollowerSize))
                nextValue = 1;
            else
            {
                tempIndex = CycleValue(currentIndex - 1, targetPoints.Length - 1);
                if (!IsTouch(eventData.position, targetPoints[tempIndex], mouseFollowerSize))
                    return;

                nextValue = -1;
            }
            nextIndex = tempIndex;
        }
        else if (!IsTouch(eventData.position, targetPoints[nextIndex], mouseFollowerSize))
            return;

        currentIndex = nextIndex;
        if (currentIndex == startIndex)
        {
            Debug.Log("is finish.");
            isFinished = true;
        }

        Vector2 point = targetPoints[currentIndex];
        finishedPoints.Add(point);
        previewPoints[0] = point;
        finishedLine.Points = finishedPoints.ToArray();

        nextIndex = CycleValue(nextIndex + nextValue, targetPoints.Length - 1);
    }

    private void OnOperateRangeEndDrag(PointerEventData eventData)
    {
        isOperate = false;
        isFinished = false;
        finishedPoints.Clear();
        //finishedLine.gameObject.SetActive(false);
        previewLine.gameObject.SetActive(false);
    }

    private bool IsTouch(Vector2 point1, Vector2 point2, float range)
    {
        float x = point1.x - point2.x;
        float y = point1.y - point2.y;
        if (x < 0)
            x = x * -1;
        if (y < 0)
            y = y * -1;

        return x <= range && y <= range;
    }

    /// <summary>
    /// When value bigger than max, value = 0,
    /// <para>value smaller than min, value = max.</para>
    /// </summary>
    private int CycleValue(int value, int max, int min = 0)
    {
        int v = (value > max) ? min : ((value < min) ? max : value);
        return v;
    }


}

public class targetPoints{

    private Vector2[] pointWood301 = new Vector2[8]
    {
        new Vector2(360,360),
        new Vector2(250,360),
        new Vector2(140,360),
        new Vector2(140,250),
        new Vector2(140,140),
        new Vector2(250,140),
        new Vector2(360,140),
        new Vector2(360,250)

    };
}




//new Vector2(810, 390),
//new Vector2(810, 690),
//new Vector2(910, 690),
//new Vector2(910, 590),
//new Vector2(1010, 590),
//new Vector2(1010, 490),
//new Vector2(1110, 490),
//new Vector2(1110, 390),