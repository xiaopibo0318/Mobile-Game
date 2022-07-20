using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class clickItem : MonoBehaviour
{



    private void OnMouseDown()
    {
        Debug.Log("點下去了");
        CanvasManager.Instance.openCanvas("House");

        openWhich(gameObject.name);

    }


    public void openWhich(string name)
    {
        switch (name)
        {
            case "houseQues":
                CanvasManager.Instance.openCanvas("House");
                break;
            case "flowerQues":
                CanvasManager.Instance.openCanvas("Flower");
                break;
            case "phoenix":
                CanvasManager.Instance.openCanvas("Phoenix");
                break;
            case "wiresawQues":
                CanvasManager.Instance.openCanvas("Wiresaw");
                break;
            
        }
            
                
    }
}
