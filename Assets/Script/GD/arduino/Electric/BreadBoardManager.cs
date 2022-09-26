using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreadBoardManager : Singleton<BreadBoardManager>
{
    
    private List<Vector2> exeList = new List<Vector2>();
    void Start()
    {

    }

    void Update()
    {

    }
    private void FindNextPoint(int now_x, int now_y)
    {
        if (CheckIsHorizontalOrVertical(now_x))
        {



        }
    }

    // true = Horizontal
    // false = Vertical
    private bool CheckIsHorizontalOrVertical(int nowRow)
    {
        if (nowRow <= 2 || nowRow >= 9) return true;
        else return false;
    }
}
