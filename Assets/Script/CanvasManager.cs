using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasManager : MonoBehaviour
{

    [SerializeField] IamCanvas[] _canvas;
    //目前五個 0基本、1鳳凰 、2手線鋸、3花、4偏房
   

    public void openCanvas(string canvasName)
    {
        //為了讓系統讀取到Menu列表，GUI上記得掛上去menu
        for (int i = 0; i < _canvas.Length; i++)
        {
            if (_canvas[i].canvasName == canvasName)
            {
                OpenCanvas(_canvas[i]);
            }
            else if (_canvas[i].open)
            {
                CloseCanvas(_canvas[i]);
            }
        }
    }


    public void OpenCanvas(IamCanvas canvas)
    {
        for (int i = 0; i < _canvas.Length; i++)
        {
            if (_canvas[i].open)
            {
                CloseCanvas(_canvas[i]);
            }
        }
        canvas.Open();
    }

    public void CloseCanvas(IamCanvas canvas)
    {
        canvas.Close();
    }

}
