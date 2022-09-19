using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class teleport : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        TransiitionManager.Instance.TPGD2();
    }
}
