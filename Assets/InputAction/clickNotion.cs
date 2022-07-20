using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class clickNotion : MonoBehaviour
{
    private void OnMouseDown()
    {
        Debug.Log("點下去了");
        CanvasManager.Instance.openCanvas("House");

        openWhichNote(gameObject.name);

    }

    public void openWhichNote(string name)
    {
        switch (name)
        {
            case "LoveNotion":
                CanvasManager.Instance.openCanvas("notion");
                break;

        }


    }

}
