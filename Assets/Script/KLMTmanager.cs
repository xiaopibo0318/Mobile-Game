using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class KLMTmanager : MonoBehaviour
{
    /*
    總管理
    */

    public enum GameStatus
    {
        beforeFlower,
        beforeHouse,
        beforeBall,

    }

    GameStatus gameStatus;
    
    public static KLMTmanager Instance;

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
            switch (gameStatus)
            {
                case GameStatus.beforeFlower:
                    break;
                case GameStatus.beforeHouse:
                    secondStepKL();
                    break;
                case GameStatus.beforeBall:
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
        gameStatus = GameStatus.beforeHouse;
        woodUnactive.SetActive(false);
        woodActive.SetActive(true);
        Debug.Log("第一階段已完成");
        teleport.SetActive(true);
    }

    //


}
