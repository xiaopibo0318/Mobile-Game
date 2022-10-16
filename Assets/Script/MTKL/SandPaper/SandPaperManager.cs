using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SandPaperManager : MonoBehaviour
{

    [Header("背包是否有此物品")]
    public Inventory myBag;
    public Item paper150;
    public Item paper240;
    public Item paper400;
    bool is150;
    bool is240;
    bool is400;
    public GameObject panel150;
    public GameObject panel240;
    public GameObject panel400;

    [Header("球的型態")]
    public Item ball0; //解除要扣掉的
    public Item ball1;
    public Item ball2;
    public Item ball3;


    [Header("處理列表")]
    List<int> exeList = new List<int>();

    private int nowBall;

    public static SandPaperManager Instance;


    public void Awake()
    {
        is150 = false;
        is240 = false;
        is400 = false;

        Instance = this;
    }

    private void OnEnable()
    {
        
        UpdateContent();
    }

    private void Update()
    {
        CheckSandPaperInBag();
    }

    public void CheckSandPaperInBag()
    {
        if (myBag.itemList.Contains(paper400))
        {
            is400 = true;
        }
        else is400 = false;
        if (myBag.itemList.Contains(paper240))
        {
            is240 = true;
        }
        else is240 = false;
        if (myBag.itemList.Contains(paper150))
        {
            is150 = true;
        }
        else is150 = false;
    }

    public void UpdateContent()
    {
        if (is150) panel150.SetActive(false);
        else panel150.SetActive(true);

        if (is240) panel240.SetActive(false);
        else panel240.SetActive(true);


        if (is400) panel400.SetActive(false);
        else panel400.SetActive(true);
    }

    public void HandleCircle(int n)
    {
        exeList.Add(n);
    }

    public void ResetCircle()
    {
        exeList.Clear();
    }

    public void IfCrackSucess()
    {
        if (nowBall == 0)
        {
            InventoryManager.Instance.AddNewItem(ball1);
        }
        else if (nowBall == 1)
        {
            InventoryManager.Instance.AddNewItem(ball2);
        }
        else if (nowBall == 2)
        {
            InventoryManager.Instance.AddNewItem(ball3);
        }
        else
        {
            cacheVisable.Instance.siginalSomething("發生錯誤");
        }
    }

    public void NowBallType(string myTag)
    {
        Debug.Log(myTag);
        if (myTag == "ball0")
        {
            nowBall = 0;
        }
        else if (myTag == "ball1")
        {
            nowBall = 1;
        }
        else if (myTag == "ball2")
        {
            nowBall = 2;
        }
        else
        {
            nowBall = 999;
        }
    }

}


