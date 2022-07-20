﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class cacheVisable : MonoBehaviour
{
    Color colorBegin;
    Color colorOriginal;

    public GameObject imageInfo;
    public Text itemInfo;
    
   Transform originalTrans;

    float timeBegin;
    float timeEnd;
    float changeOpacity;
    float changePos;
    float visibaleTime;
    
    bool isVisable;
    bool isInfoFinished;
    public void Awake()
    {
        colorOriginal = imageInfo.GetComponent<Image>().color;
        colorBegin = new Color(colorOriginal.r, colorOriginal.g, colorOriginal.b, .0f);
        imageInfo.SetActive(false);
        //itemInfo.text = " ";
        originalTrans.position = imageInfo.GetComponent<RectTransform>().position;
        changePos = 3;
    }


    public void FixedUpdate()
    {
        if (isVisable && !isInfoFinished)
        {
            StopCoroutine(changeColor());
            isInfoFinished = true;
        }
    }


    public void cacheSomething(Item item)
    {
        isVisable = true;
        isInfoFinished = false;
        itemInfo.text ="獲得物品："+ item.itemInfo + " x1";
        imageInfo.GetComponent<Image>().color = colorBegin;
        StartCoroutine(changeColor());
    }

    IEnumerator changeColor()
    {
        while (changeOpacity<1)
        {
            yield return new WaitForSeconds(0.1f);
            imageInfo.GetComponent<Image>().color = new Color(255,255,255,changeOpacity);
            changeOpacity += 0.1f;
        }
        while (visibaleTime > 0)
        {
            yield return new WaitForSeconds(0.1f);
            visibaleTime -= 1;
        }
        while (changeOpacity >0)
        {
            yield return new WaitForSeconds(0.1f);
            imageInfo.GetComponent<Image>().color = new Color(255, 255, 255, changeOpacity);
            changeOpacity -= 0.1f;
            imageInfo.GetComponent<RectTransform>().position = new Vector3(originalTrans.position.x, originalTrans.position.y - changePos, originalTrans.position.z);
        }
        isInfoFinished = true;
    }

}
