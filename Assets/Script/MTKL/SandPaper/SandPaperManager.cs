using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SandPaperManager : MonoBehaviour
{
    [Header("砂紙類型")]
    public GameObject paper_150;


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

    public static SandPaperManager Instance;


    public void Awake()
    {
        is150 = false;
        is240 = false;
        is400 = false;

        Instance = this;
    }

    private void FixedUpdate()
    {
        CheckSandPaperInBag();
        UpdateContent();
    }


    public void CheckSandPaperInBag()
    {
      if (myBag.itemList.Contains(paper400))
      {
            is400 = true;
      }
      if (myBag.itemList.Contains(paper240))
      {
            is240 = true;
      }
      if (myBag.itemList.Contains(paper150))
      {
            is150= true;
      }
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
}



public class ballStaus{
    
 


    public void newBallStatus()
    {

    }


}