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


    Vector3 originalPos;

    public static cacheVisable Instance;

    public void Awake()
    {
        colorOriginal = imageInfo.GetComponent<Image>().color;
        colorBegin = new Color(colorOriginal.r, colorOriginal.g, colorOriginal.b, .0f);
        originalTrans = imageInfo.GetComponent<RectTransform>();
        originalPos = originalTrans.localPosition;
        
        itemInfo.text = " ";
        imageInfo.SetActive(false);
        
        changePos = 3;
        Instance = this;
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
        visibaleTime = 1.5f;
        isVisable = true;
        isInfoFinished = false;
        imageInfo.SetActive(true);
        itemInfo.text ="獲得物品："+ item.itemName + " x1";
        imageInfo.GetComponent<Image>().color = colorBegin;
        StartCoroutine(changeColor());
    }


    public void siginalSomething(string myText)
    {
        visibaleTime = 10;
        isVisable = true;
        isInfoFinished = false;
        imageInfo.SetActive(true);
        itemInfo.text = myText;
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
            visibaleTime -= 0.1f;
        }
        while (changeOpacity >0)
        {
            yield return new WaitForSeconds(0.1f);
            imageInfo.GetComponent<Image>().color = new Color(255, 255, 255, changeOpacity);
            changeOpacity -= 0.1f;
            imageInfo.GetComponent<RectTransform>().position = new Vector3(originalTrans.position.x, originalTrans.position.y + changePos, originalTrans.position.z);
        }
        itemInfo.text = "";
        //imageInfo.GetComponent<RectTransform>().position = new Vector3(900, 844.77f, 0);
        originalTrans.localPosition = originalPos;
        isInfoFinished = true;
        
    }

}
