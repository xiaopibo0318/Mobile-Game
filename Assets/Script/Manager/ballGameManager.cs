using System.Collections;
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

    [Header("背包裡的球圖片")]
    public GameObject ball1;
    public GameObject ball2;
    public GameObject ball3;
    public GameObject Content;

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
        
    }



    public void StartGame()
    {
        if (nowType != 0)
        {
            if (!isStart)
            {
                ballType[nowType-1].SetActive(true);
                ResetPosition();
                ballType[nowType-1].GetComponent<Rigidbody2D>().gravityScale = 20;
                isStart = true;
            }
        }
        
        
        
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
        }else if (n== "ball2")
        {
            ball1.SetActive(false);
            ball2.SetActive(true);
            ball3.SetActive(false);
            nowType = 2;
        }else if ( n == "ball3")
        {
            ball1.SetActive(false);
            ball2.SetActive(false);
            ball3.SetActive(true);
            nowType = 3;
        }
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
}
