using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class KLMTmanager : MonoBehaviour
{
    /*
    總管理
    */



    public static KLMTmanager Instance;

    public int gameStatus { get; set; }

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
            gameStatus = Player.Instance.myStatus.GetGameStatus();
            switch (gameStatus)
            {
                case 2:    //第一階段
                    break;
                case 12:    //第二階段
                    secondStepKL();
                    break;
                case 13:
                    thirdStepKL();
                    break;
            }


        }

    }

    private void OnEnable()
    {
        Camerafollowww.Instance.UpdateNowSceneBuildIndex();
        TimeCounter.Instance.StartCountDown();
        DialogueMTKL.Instance.ChangeChatStatus();
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
    [SerializeField] private GameObject endObject;


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
        Player.Instance.myStatus.ChangeGameStatus(12);
        DialogueMTKL.Instance.ChangeChatStatus();
    }

    //前往第二場景
    public void thirdStepKL()
    {
        woodActive.SetActive(false);
        woodAnswer.SetActive(true);
        teleport.SetActive(true);
        Player.Instance.myStatus.ChangeGameStatus(13);
        DialogueMTKL.Instance.ChangeChatStatus();
    }

    private void endStepKL()
    {
        endObject.SetActive(true);
    }

    private int GetGameStatus()
    {
        return Player.Instance.myStatus.GetGameStatus();
    }

    public void KLMTinitial()
    {
        Debug.Log("QQQ");
        switch (gameStatus)
        {
            case 1:    //第一階段
                break;
            case 12:    //第二階段
                Debug.Log("重製至第二階段");
                secondStepKL();
                break;
            case 13:
                thirdStepKL();
                break;
            case 14:
                endStepKL();
                break;

        }

        Player.Instance.myStatus.gameStatus = gameStatus;
    }
}


