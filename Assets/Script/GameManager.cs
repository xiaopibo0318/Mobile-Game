using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    /*
    總管理
    */




    
    public static GameManager Instance;

    public void Awake()
    {
        if(Instance != null)
        {
            Destroy(gameObject);
        }
        Instance = this;

        Scene scene = SceneManager.GetActiveScene();
        if (scene.name == "MtKL1")
        {
            firstStepKL();
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
    }

    //


}
