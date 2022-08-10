using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cutEdge : MonoBehaviour
{
    private void OnTriggerExit2D(Collider2D collision)
    {
        Debug.Log("碰到牆壁啦");
        DrawWoodLine.Insatnce.ClearLine();
    }

    private void OnColliderEnter2D(Collider2D collision)
    {
        Debug.Log("碰到牆壁啦");
        DrawWoodLine.Insatnce.ClearLine();
    }

   
}
