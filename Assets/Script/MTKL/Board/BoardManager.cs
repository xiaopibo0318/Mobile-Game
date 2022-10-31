using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TouchEvent_Handler;
using DG.Tweening;

public class BoardManager : EventDetect
{
    public List<RectTransform> myNews;
    public GameObject goBack;
    public GameObject backGround;
    private int currentID;

    private RectTransform originalTransform;

    public void Awake()
    {
        CloseBoard();
    }


    public void OpenNews(int n)
    {
        myNews[n].gameObject.SetActive(true);
        goBack.SetActive(true);
        backGround.SetActive(true);
        currentID = n;
    }


    public void CloseBoard()
    {
        for (int i = 0; i < myNews.Count; i++)
        {
            myNews[i].gameObject.SetActive(false);
            goBack.SetActive(false);
            backGround.SetActive(false);
        }
    }

    public override void Init()
    {
        originalTransform = myNews[currentID];
        myNews[currentID].anchorMin = Vector2.zero;
        myNews[currentID].anchorMax = Vector2.zero;
    }

    public override void ChangeScale(Touch touch1, Touch touch2)
    {
        myNews[currentID].anchorMin = Vector2.zero;
        myNews[currentID].anchorMax = Vector2.zero;
        myNews[currentID].DOScale(scaleOffset, .2f);
        Vector3 newPivotPos = (touch1.position + touch2.position) / 2;
        myNews[currentID].anchoredPosition = newPivotPos;
    }

    public override void ResetImage()
    {
        myNews[currentID].localPosition = originalTransform.localPosition;
        myNews[currentID].anchoredPosition = originalTransform.anchoredPosition;

    }

}
