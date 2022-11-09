using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerHandleZone : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        RayCastManager.Instance.playerObject = collision.gameObject;
        RayCastManager.Instance.HandleWarning();
    }
}
