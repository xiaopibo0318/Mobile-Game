using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class teleport : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.position.x > -50) TransiitionManager.Instance.TPGD2();
        else TransiitionManager.Instance.TPGD1();

    }
}
