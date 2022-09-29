using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectricSlot : MonoBehaviour
{
    public int row { get; set; }
    public int col { get; set; }

    [SerializeField] GameObject normal;
    [SerializeField] GameObject hoover;
    [SerializeField] GameObject active;
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        normal.SetActive(false);
        hoover.SetActive(true);
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        normal.SetActive(true);
        hoover.SetActive(false);
    }

    public void ChangeToActive()
    {
        hoover.SetActive(false);
        active.SetActive(true);
    }

}
