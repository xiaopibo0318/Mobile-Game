﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IamCanvas : MonoBehaviour
{
    public string canvasName;
    public bool open;

    public void Open()
    {
        open = true;
        gameObject.SetActive(true);
    }
    public void Close()
    {
        open = false;
        gameObject.SetActive(false);
    }

    
}
