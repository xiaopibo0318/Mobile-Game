using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ballGoal : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        ballGameManager.Instance.EndGame();
    }
}
