﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class KLMTmanager : MonoBehaviour
{
    /*
    總管理
    */


    
    public static KLMTmanager Instance;

    int gameStatus;  

    public void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this);
        }
        else if (this != Instance)
        {
            Destroy(gameObject);
        }

        Scene scene = SceneManager.GetActiveScene();
        if (scene.name == "MtKL1")
        {
            switch (Player.Instance.myStatus.GetGameStatus())
            {
                case  1:    //第一階段
                    break;
                case 12:    //第二階段
                    secondStepKL();
                    break;
            }
                

        }

    }

    private void Update()
    {
        
    }



    /*
    崑崙山管理 
    */

    [Header("崑崙山組件")]
    public GameObject woodUnactive;
    public GameObject woodActive;
    public GameObject woodAnswer;
    public GameObject teleport;
    //public GameObject flowerClose;
    //public GameObject flowerOpen;


    //蓮花謎題
    public void firstStepKL()
    {
        CanvasManager.Instance.openCanvas("original");
    }

    //木頭謎題
    public void secondStepKL()
    {
        woodUnactive.SetActive(false);
        woodActive.SetActive(true);
        Debug.Log("第一階段已完成");
        teleport.SetActive(true);
        Player.Instance.myStatus.ChangeGameStatus(12);
    }

    private int GetGameStatus()
    {
        return Player.Instance.myStatus.GetGameStatus();
    }

    public void KLMTinitial()
    {
        if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            switch (Player.Instance.myStatus.GetGameStatus())
            {
                case 1:    //第一階段
                    break;
                case 12:    //第二階段
                    Debug.Log("重製至第二階段");
                    secondStepKL();
                    break;
            }


        }
    }
}


