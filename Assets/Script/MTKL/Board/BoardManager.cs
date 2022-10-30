using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TouchEvent_Handler;
using DG.Tweening;

public class BoardManager : EventDetect
{
    public List<GameObject> myNews;
    public GameObject goBack;
    public GameObject backGround;
    private int currentID;

    public void Awake()
    {
        CloseBoard();
    }


    public void OpenNews(int n)
    {
        myNews[n].SetActive(true);
        goBack.SetActive(true);
        backGround.SetActive(true);
        currentID = n;
    }


    public void CloseBoard()
    {
        for (int i = 0; i < myNews.Count; i++)
        {
            myNews[i].SetActive(false);
            goBack.SetActive(false);
            backGround.SetActive(false);
        }
    }

    public override void Bigger()
    {
        myNews[currentID].transform.DOScale(scaleOffset, .2f);
    }

    public override void Smaller()
    {
        base.Smaller();
    }
}
