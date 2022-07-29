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

    public void Awake()
    {
        answer = new int[8] { 0, 0, 1, 1, 0, 1, 0, 1};
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
            firstStep = false;
            Debug.Log("第一道關卡以解決");
            flowerUnactivate.SetActive(false);
            flowerActivate.SetActive(true);
            flowerUnactivateLook.SetActive(false);
            flowerActivateLook.SetActive(true);
            //KLMTmanager.Instance.gameStatus = KLMTmanager.GameStatus.beforeHouse;
            KLMTmanager.Instance.secondStepKL();
            

        }else
        {
            Debug.Log("失敗");
        }
        index = 0;
    }





}
