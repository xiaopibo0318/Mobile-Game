using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyBoard 
{
    private static int row = 12; // 10+2
    private static int col = 8;  // 6+2

    /*
    設計概念：
    建立布林表偵測是否有物件。

    再每一個電子元件中 設立 point1 與 point2
    當
     */
    private bool[,] isObjectInBoard = new bool[row,col];
    private bool[,] isElectric = new bool[row, col];

    private void Init()
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
                isElectric[i, j] = false;
            }
        }
    }

}
