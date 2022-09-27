using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectricSlot
{
    private int row;
    private int col;

    /// <summary>
    /// 1 = 正電拍拍 0 = 負電拍拍 -99 = 不通電
    /// </summary>
    private int electricMode;

    public void Init(int _row, int _col)
    {
        this.row = _row;
        this.col = _col;
        this.electricMode = -99;
    }

    private void ChangeElectricMode(int electricType)
    {
        this.electricMode = electricType;
    }
}
