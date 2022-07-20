using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
        
        if (!isStart)
        {
            ballType[0].SetActive(true);
            ResetPosition();
            ballType[0].GetComponent<Rigidbody2D>().gravityScale = 20;
            isStart = true;
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
        ballType[0].GetComponent<Rigidbody2D>().gravityScale = 0;
        ResetPosition();
    }


    public void ResetPosition()
    {
        topGroundRotateNum = 0;
        downGroundRotateNum = 0;
        ballType[0].GetComponent<RectTransform>().localPosition = new Vector3(-205, 369, 0);
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


}
