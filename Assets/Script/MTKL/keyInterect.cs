using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class keyInterect : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        itemUIManager.Instance.KeyUsable(true);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        itemUIManager.Instance.KeyUsable(false);
    }
}
