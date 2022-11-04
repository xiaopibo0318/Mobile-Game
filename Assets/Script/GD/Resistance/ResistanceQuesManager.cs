﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TouchEvent_Handler;
using DG.Tweening;
using UnityEngine.EventSystems;

public class ResistanceQuesManager : EventDetect
{
    [Header("圖片素材")]
    public List<Transform> allPicTransform = new List<Transform>();

    public Image colorImage;

    [Header("按鈕")]
    [SerializeField] private Button enterButton;
    [SerializeField] private Button goBackButton;
    [SerializeField] private Button detectButton;

    [Header("判定區域")]
    private int currentID;
    [SerializeField] private InputField answerZone1;
    [SerializeField] private InputField answerZone2;
    [Header("UI階段")]
    [SerializeField] private GameObject quesChoose;
    [SerializeField] private GameObject quesAnswer;


    private List<Vector3> imagePos = new List<Vector3>();

    public static ResistanceQuesManager Instance;
    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(this);
    }

    public int CurrentID
    {
        get => currentID;
        set { currentID = value; }
    }


    private void OnEnable()
    {
        for (int i = 0; i < allPicTransform.Count; i++)
        {
            imagePos.Add(allPicTransform[i].position);
        }
        ButtonInit();
        GoBackToChoose();
        quesChoose.gameObject.SetActive(false);
        quesAnswer.gameObject.SetActive(false);
    }


    private void ButtonInit()
    {
        enterButton.onClick.AddListener(GoToAnswer);
        goBackButton.onClick.AddListener(GoBackToChoose);
        goBackButton.gameObject.SetActive(false);
        detectButton.onClick.AddListener(DetectAnswer);
    }



    private void GoToAnswer()
    {
        quesAnswer.SetActive(true);
        Vector3 endPos = new Vector3(300, 700, 0);
        for (int i = 0; i < allPicTransform.Count; i++)
        {
            if (i == currentID)
            {
                allPicTransform[currentID].DOMove(endPos, .8f);
            }
            else
            {
                allPicTransform[i].gameObject.SetActive(false);
            }
        }
        enterButton.gameObject.SetActive(false);
        goBackButton.gameObject.SetActive(true);
        detectButton.gameObject.SetActive(true);
        answerZone1.gameObject.SetActive(true);
        answerZone2.gameObject.SetActive(true);
        colorImage.gameObject.SetActive(true);

    }

    private void GoBackToChoose()
    {
        for (int i = 0; i < allPicTransform.Count; i++)
        {
            allPicTransform[i].gameObject.SetActive(true);
            allPicTransform[i].DOMove(imagePos[i], .8f);
        }
        enterButton.gameObject.SetActive(true);
        goBackButton.gameObject.SetActive(false);
        answerZone1.gameObject.SetActive(false);
        answerZone2.gameObject.SetActive(false);
        detectButton.gameObject.SetActive(false);
        colorImage.gameObject.SetActive(false);
    }


    private void DetectAnswer()
    {
        string answer1 = answerZone1.text;
        string answer2 = answerZone2.text;
        switch (currentID)
        {
            case 0:
                if (answer1 == "2.4M" || answer1 == "2400K" || answer1 == "2400000")
                {
                    if (answerZone2.text == "5")
                    {
                        SiginalUI.Instance.SiginalText("成功");
                    }

                }
                break;
            case 1:
                break;
        }
    }


    public override void PointerDown(PointerEventData eventData)
    {
        base.PointerDown(eventData);
        for (int i = 0; i < allPicTransform.Count; i++)
        {
            if (eventData.pointerCurrentRaycast.gameObject == allPicTransform[i].gameObject)
            {
                currentID = i;
                
            }
        }

        for (int i = 0; i < allPicTransform.Count; i++)
        {
            if (i == currentID) allPicTransform[currentID].DOScale(1.1f, 0.6f);
            else allPicTransform[i].DOScale(1, 0.6f);
        }

    }

    public void OpenQues()
    {
        quesChoose.SetActive(true);
    }

}


