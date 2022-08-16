using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WoodDetectOutside : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("碰到牆壁了11");
       // DrawWoodLine.Insatnce.CutFail();
    }
}
