using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour
{
    bool isInZone;

    public static Goal Instance;

    private void Awake()
    {
        isInZone = false;
        Instance = this;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        isInZone = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        isInZone = false;
    }

    public bool getInZone()
    {
        return isInZone;
    }

}
