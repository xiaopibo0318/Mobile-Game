﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ballGameManager : MonoBehaviour
{
    public GameObject[] ballType;

    public GameObject upGround, downGround;

    public static ballGameManager Instance;

    bool isStart;


    [Header("板子狀態")]
    public bool topLeft;
    public bool topRight;
    public bool downLeft;
    public bool downRight;

    [Header("背包裡的球圖片與遊戲上")]
    public GameObject ball1;
    public GameObject ball2;
    public GameObject ball3;
    public GameObject Content;

    [Header("判斷背包是否有球")]
    public GameObject panel1;
    public GameObject panel2;
    public GameObject panel3;
    public Item myBall1;
    public Item myBall2;
    public Item myBall3;
    public Inventory myBag;

    int nowType;

    public Item Key;

    float topGroundRotateNum, downGroundRotateNum;

    Transform upGrondTrans, downGroundTrans;

    void Awake()
    {
        isStart = false;
        Instance = this;

        topGroundRotateNum = 0;
        downGroundRotateNum = 0;
        upGrondTrans = upGround.transform;
        downGroundTrans = downGround.transform;
        nowType = 0;
        ReStartGame();
    }


    public void FixedUpdate()
    {
        if (isStart)
        {
            RotateBoard();
        }
        CheckBallInBag();
    }



    public void StartGame()
    {
        if (nowType != 0)
        {
            if (!isStart)
            {

                InventoryManager.Instance.SubItem(GetNowBall(nowType));
                ballType[nowType-1].SetActive(true);
                ResetPosition();
                ballType[nowType-1].GetComponent<Rigidbody2D>().gravityScale = 20;
                isStart = true;
            }
        }
        else
        {
            //之後要換大字
            cacheVisable.Instance.siginalSomething("還沒選擇球球喔");
        }
        
    }

    public Item GetNowBall(int NowType)
    {
        if (NowType == 1) return myBall1;
        else if (NowType == 2) return myBall2;
        else if (NowType == 3) return myBall3;
        else return null;
    }

    /// <summary>
    /// 四個板子的旋轉狀態
    /// </summary>
    /// <param name="goRotate"></param>

    public void rotateLeftUP(bool goRotate)
    {
        if (goRotate)
            topLeft = true;
        else
            topLeft = false;
 
    }
    public void rotateRightUP(bool goRotate)
    {
        if (goRotate)
            topRight = true;
        else
            topRight = false;
    }
    public void rotateLeftDown(bool goRotate)
    {
        if (goRotate)
            downLeft = true;
        else
            downLeft = false;

    }
    public void rotateRightDown(bool goRotate)
    {
        if (goRotate)
            downRight = true;
        else
            downRight = false;
    }



    public void ReStartGame()
    {
        isStart = false;
        if(nowType != 0)
        {
            ballType[nowType - 1].GetComponent<Rigidbody2D>().gravityScale = 0;
        }
            
        ResetPosition();
    }


    public void ResetPosition()
    {
        topGroundRotateNum = 0;
        downGroundRotateNum = 0;
        if (nowType != 0)
        {
            ballType[nowType-1].GetComponent<RectTransform>().localPosition = new Vector3(-205, 369, 0);
        }
       
        upGround.transform.rotation = Quaternion.Euler(0, 0, topGroundRotateNum);
        downGround.transform.rotation = Quaternion.Euler(0, 0, topGroundRotateNum);
    }

    /// <summary>
    /// 旋轉板子
    /// </summary>
    public void RotateBoard()
    {
        if (topLeft)
        {
            topGroundRotateNum -= 0.1f;
            upGround.transform.rotation = Quaternion.Euler(0, 0, topGroundRotateNum);
        }
        else if (topRight)
        {
            topGroundRotateNum += 0.1f;
            upGround.transform.rotation = Quaternion.Euler(0, 0, topGroundRotateNum);
        }
        else if (downLeft)
        {
            downGroundRotateNum -= 0.1f;
            downGround.transform.rotation = Quaternion.Euler(0, 0, downGroundRotateNum);

        }
        else if (downRight)
        {
            downGroundRotateNum += 0.1f;
            downGround.transform.rotation = Quaternion.Euler(0, 0, downGroundRotateNum);
        }
        else return;
        
    }


    public void EndGame()
    {
        InventoryManager.Instance.AddNewItem(Key);
    }

    public void BallOnClick()
    {
        var n = EventSystem.current.currentSelectedGameObject.name;

        if (n == "ball1")
        {
            ball1.SetActive(true);
            ball2.SetActive(false);
            ball3.SetActive(false);
            nowType = 1;
        }
        else if (n == "ball2")
        {
            ball1.SetActive(false);
            ball2.SetActive(true);
            ball3.SetActive(false);
            nowType = 2;
        }
        else if (n == "ball3")
        {
            ball1.SetActive(false);
            ball2.SetActive(false);
            ball3.SetActive(true);
            nowType = 3;
        }
        else nowType = 0;
    }

    public void openBackPack()
    {
        if(Content.activeInHierarchy== true)
        {
            Content.SetActive(false);
        } else
        {
            Content.SetActive(true);
        }
    }

    public void CheckBallInBag()
    {
        if (myBag.itemList.Contains(myBall1))
        {
            panel1.SetActive(false);
        }
        else panel1.SetActive(true);
        if (myBag.itemList.Contains(myBall2))
        {
            panel2.SetActive(false);
        }
        else panel2.SetActive(true);
        if (myBag.itemList.Contains(myBall3))
        {
            panel3.SetActive(false);
        }
        else panel3.SetActive(true);
    }

   
}
