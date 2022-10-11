using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowerManager : MonoBehaviour
{
    [SerializeField] FlowerOrgan[] flowerType;
    private int[] answer;
    private int[] myAnswer;
    [SerializeField] GameObject flowerUnactivate;
    [SerializeField] GameObject flowerActivate;
    [SerializeField] GameObject flowerUnactivateLook;
    [SerializeField] GameObject flowerActivateLook;
    int index;
    bool firstStep;
    bool isEqual;

    [Header("臨時的東西")]
    public Item ball;

    public void Awake()
    {
        answer = new int[8] { 1, 0, 1, 0, 0, 0, 1, 1};   //0,2,6,7  對應 原始密碼2,3,5,7 => 變換的結果。
        myAnswer = new int[8] { 0, 0, 0, 0, 0, 0, 0, 0 };
        index = 0;
        firstStep = true;
        flowerUnactivate.SetActive(true);
        flowerActivate.SetActive(false);
        flowerUnactivateLook.SetActive(true);
        flowerActivateLook.SetActive(false);
    }

    public void FixedUpdate()
    {

            

        
    }

    public void CheckAnswer()
    {
        for (int i = 0; i < flowerType.Length; i++)
        {
            if (flowerType[i]._click)
            {
                myAnswer[i] = 1;
            }
            else if (!flowerType[i]._click)
            {
                myAnswer[i] = 0;
            }
        }


        for (int i = 0; i < flowerType.Length; i++)
        {
            if (answer[i] == myAnswer[i]) index += 1;
        }

        if (index == 8)
        {
            AudioManager.Instance.lotus("susscess");
            firstStep = false;
            Debug.Log("第一道關卡以解決");
            flowerUnactivate.SetActive(false);
            flowerActivate.SetActive(true);
            flowerUnactivateLook.SetActive(false);
            flowerActivateLook.SetActive(true);
            cacheVisable.Instance.siginalSomething("偏房似乎有了變化，去看看吧！");
            //KLMTmanager.Instance.gameStatus = KLMTmanager.GameStatus.beforeHouse;
            KLMTmanager.Instance.secondStepKL();
            

        }else
        {
            AudioManager.Instance.lotus("failed");
            Debug.Log("失敗");
        }
        index = 0;
    }





}
