using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasManager : MonoBehaviour
{

    [SerializeField] IamCanvas[] _canvas;
    //目前五個 0基本、1鳳凰 、2手線鋸、3花、4偏房

    public static CanvasManager Instance;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            //DontDestroyOnLoad(this);
        }
        else if (this != Instance)
        {
            //Destroy(gameObject);
        }
        openCanvas("original");
    }

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

    public void closeCanvas(string canvasName)
    {
        for (int i = 0; i < _canvas.Length; i++)
        {
            if(canvasName == _canvas[i].name)
            {
                CloseCanvas(_canvas[i]);
                break;
            }
        }
    }

    public void CloseAllCanvas()
    {
        for (int i = 0; i < _canvas.Length; i++)
        {
             CloseCanvas(_canvas[i]);
        }
    }

}
