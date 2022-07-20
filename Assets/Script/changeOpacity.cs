using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class changeOpacity : MonoBehaviour
{
    Color originalColor;
    Color changeColor;

    private void Awake()
    {
        originalColor = GetComponent<SpriteRenderer>().color;
        changeColor = new Color(originalColor.r, originalColor.g, originalColor.b, .30f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        gameObject.GetComponent<SpriteRenderer>().color = changeColor; 
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        gameObject.GetComponent<SpriteRenderer>().color = originalColor;
    }

}
