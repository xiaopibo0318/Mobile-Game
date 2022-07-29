using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class deathEdge : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        ballGameManager.Instance.ReStartGame();
    }
}
