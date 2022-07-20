using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraFollow : MonoBehaviour
{
    public Transform target;
    public float smoothing;

    private void LateUpdate()
    {
        if (transform.position != target.position)
        {
            Vector3 targetPos = target.position;
            transform.position = Vector3.Lerp(transform.position, targetPos, smoothing);
        }

    }
}
