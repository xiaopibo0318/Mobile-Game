using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpacityWithTrigger : MonoBehaviour
{
    Color colorBegin;
    Color colorEnd;

    void Awake()
    {
        colorBegin = GetComponent<SpriteRenderer>().color;
        colorEnd = new Color(colorBegin.r, colorBegin.g, colorBegin.b, .30f);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {

        gameObject.GetComponent<SpriteRenderer>().color = colorEnd;

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        gameObject.GetComponent<SpriteRenderer>().color = colorBegin;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        gameObject.GetComponent<SpriteRenderer>().color = colorEnd;
    }
}
