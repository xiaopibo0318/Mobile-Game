using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyBoard 
{
    public static int row = 10; 
    public static int col = 6;  

    
    /*
    設計概念：
    建立布林表偵測是否有物件。

    再每一個電子元件中 設立 point1 與 point2
    並把每個hole插入電裡 1= 正極 0 = 負極 -99 =不通電

    電子元件固定在麵包版上
    僅接線

     */
    public bool[,] isObjectInBoard = new bool[row,col];
    public int[,] isElectric = new int[row, col];

    public void Init()
    {
        for (int i = 0; i < row; i++)
        {
            for (int j = 0; j < col; j++)
            {
                isObjectInBoard[i, j] = false;
            }
        }

        for (int i = 0; i < row; i++)
        {
            for (int j = 0; j < col; j++)
            {
                isElectric[i, j] = -99;
            }
        }
    }

    public int GetBoardRow()
    {
        return row;
    }

    public int GetBoardCol()
    {
        return col;
    }

    public void ChangeObjectInBoard(int _row, int _col)
    {
        isObjectInBoard[_row, _col] = true;
    }

    private void ResetAll()
    {
        Init();
    }
}
