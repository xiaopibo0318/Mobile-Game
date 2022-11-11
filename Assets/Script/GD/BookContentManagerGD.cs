using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BookContentManagerGD : Singleton<BookContentManagerGD>
{
    [Header("已經激活之知識")]
    public List<GameObject> myKnowledge = new List<GameObject>();
    string nowKnowledge;


    [Header("所有知識")]
    public List<GameObject> allKnwledge = new List<GameObject>();
    private List<Sprite[]> allKnowledfeSprie = new List<Sprite[]>();
    private int currentKnowledgeID;

    [Header("知識存放處")]
    private Sprite[] ArduinoKnowledge;
    private Sprite[] LEDKnowledge;
    private Sprite[] ResistanceKnowledge;
    private Sprite[] MotorSetKnowledge;
    private Sprite[] ServerMotorKnowledge;
    private Sprite[] StringKnowledge;
    private Sprite[] ButtonKnowledge;
    private Sprite[] ButtonLEDKnowledge;
    private Sprite[] RaycastKnowledge;
    private Sprite[] UltrasoundKnowledge;
    private Sprite[] BuzzerKnowledge;
    private Sprite[] HandleElectricKnowledge;
    private Sprite[] ElectricConnectKnowledge;
    private Sprite[] BreadboardKnowledge;

    [Header("頁面顯示")]
    public Image ImgRight;
    public Image ImgLeft;
    int currentID;
    public GameObject menu;
    bool nextPageFirst;
    private bool isChooseFromMenu = true;



    private void Start()
    {
        LoadSprite();
        InitSpriteToList();

        currentID = 0;

        menu.SetActive(true);

        ImgLeft.color = new Color(255, 255, 255, 0);
        ImgRight.color = new Color(255, 255, 255, 0);

        for (int i = 0; i < myKnowledge.Count; i++)
        {
            myKnowledge[i].SetActive(false);
            allKnwledge[i].SetActive(false);
        }



    }

    private void LoadSprite()
    {
        ArduinoKnowledge = Resources.LoadAll<Sprite>("Book/Arduino");
        LEDKnowledge = Resources.LoadAll<Sprite>("Book/LED");
        BreadboardKnowledge = Resources.LoadAll<Sprite>("Book/Breadboard");
        ButtonKnowledge = Resources.LoadAll<Sprite>("Book/Button");
        ButtonLEDKnowledge = Resources.LoadAll<Sprite>("Book/ButtonLED");
        BuzzerKnowledge = Resources.LoadAll<Sprite>("Book/Buzzer");
        ElectricConnectKnowledge = Resources.LoadAll<Sprite>("Book/ElectricConnect");
        HandleElectricKnowledge = Resources.LoadAll<Sprite>("Book/HandleElectric");
        MotorSetKnowledge = Resources.LoadAll<Sprite>("Book/MotorSet");
        RaycastKnowledge = Resources.LoadAll<Sprite>("Book/Raycast");
        ResistanceKnowledge = Resources.LoadAll<Sprite>("Book/Resistance");
        ServerMotorKnowledge = Resources.LoadAll<Sprite>("Book/ServerMotor");
        StringKnowledge = Resources.LoadAll<Sprite>("Book/String");
        UltrasoundKnowledge = Resources.LoadAll<Sprite>("Book/UltraSound");
    }

    private void InitSpriteToList()
    {
        allKnowledfeSprie.Add(ArduinoKnowledge);
        allKnowledfeSprie.Add(LEDKnowledge);
        allKnowledfeSprie.Add(BreadboardKnowledge);
        allKnowledfeSprie.Add(ButtonKnowledge);
        allKnowledfeSprie.Add(ButtonLEDKnowledge);
        allKnowledfeSprie.Add(BuzzerKnowledge);
        allKnowledfeSprie.Add(ElectricConnectKnowledge);
        allKnowledfeSprie.Add(HandleElectricKnowledge);
        allKnowledfeSprie.Add(MotorSetKnowledge);
        allKnowledfeSprie.Add(RaycastKnowledge);
        allKnowledfeSprie.Add(ResistanceKnowledge);
        allKnowledfeSprie.Add(ServerMotorKnowledge);
        allKnowledfeSprie.Add(StringKnowledge);
        allKnowledfeSprie.Add(UltrasoundKnowledge);
    }


    public void Onclick(string name)
    {
        for (int i = 0; i < myKnowledge.Count; i++)
        {
            if (name == myKnowledge[i].name)
            {
                myKnowledge[i].SetActive(true);
                nowKnowledge = myKnowledge[i].name;
                UpdateNowKnowledgeID(nowKnowledge);
            }
            else
            {
                myKnowledge[i].SetActive(false);
            }
        }
    }


    public void ActivateKnowledge(int i)
    {
        allKnwledge[i].SetActive(true);

    }

    public void GoNextPage()
    {
        if (nowKnowledge == null) return;

        menu.SetActive(false);
        ImgLeft.color = new Color(255, 255, 255, 255);
        ImgRight.color = new Color(255, 255, 255, 255);

        currentID += 2;
        if (currentID < allKnowledfeSprie[currentKnowledgeID].Length - 1)
        {
            if (isChooseFromMenu)
            {
                currentID = 0;
                ImgLeft.sprite = allKnowledfeSprie[currentKnowledgeID][currentID];
                ImgRight.sprite = allKnowledfeSprie[currentKnowledgeID][currentID + 1];
                isChooseFromMenu = false;
                return;
            }
            ImgLeft.sprite = allKnowledfeSprie[currentKnowledgeID][currentID];
            ImgRight.sprite = allKnowledfeSprie[currentKnowledgeID][currentID + 1];
        }
        if (currentID < 0 || currentID > allKnowledfeSprie[currentKnowledgeID].Length - 1)
        {
            currentID = 0;
            ImgLeft.color = new Color(255, 255, 255, 0f);
            ImgRight.color = new Color(255, 255, 255, 0f);
            nextPageFirst = false;
            menu.SetActive(true);
            isChooseFromMenu = true;

        }


    }

    public void GoBackPage()
    {
        //防止首頁還上一頁的問題
        if (menu.activeInHierarchy) return;
        if (nowKnowledge == null) return;

        menu.SetActive(false);
        ImgLeft.color = new Color(255, 255, 255, 255);
        ImgRight.color = new Color(255, 255, 255, 255);



        currentID -= 2;
        if (currentID > -1)
        {

            ImgLeft.sprite = allKnowledfeSprie[currentKnowledgeID][currentID];
            ImgRight.sprite = allKnowledfeSprie[currentKnowledgeID][currentID + 1];
        }
        if (currentID < 0 || currentID > allKnowledfeSprie[currentKnowledgeID].Length - 1)
        {
            currentID = 0;
            ImgLeft.color = new Color(255, 255, 255, 0f);
            ImgRight.color = new Color(255, 255, 255, 0f);
            menu.SetActive(true);
            isChooseFromMenu = true;
        }


        //用過一次返回記得退掉。
        nextPageFirst = false;
    }


    private void UpdateNowKnowledgeID(string name)
    {
        if (name.Contains("Arduino")) currentKnowledgeID = 0;
        else if (name.Contains("LED")) currentKnowledgeID = 1;
        else if (name.Contains("Bread")) currentKnowledgeID = 2;
        else if (name.Contains("ButtonKnow")) currentKnowledgeID = 3;
        else if (name.Contains("ButtonLedKnow")) currentKnowledgeID = 4;
        else if (name.Contains("Buzzer")) currentKnowledgeID = 5;
        else if (name.Contains("ElectricConnect")) currentKnowledgeID = 6;
        else if (name.Contains("HandleElectric")) currentKnowledgeID = 7;
        else if (name.Contains("MotorSet")) currentKnowledgeID = 8;
        else if (name.Contains("Raycast")) currentKnowledgeID = 9;
        else if (name.Contains("Resistance")) currentKnowledgeID = 10;
        else if (name.Contains("ServerMotor")) currentKnowledgeID = 11;
        else if (name.Contains("String")) currentKnowledgeID = 12;
        else if (name.Contains("Ultra")) currentKnowledgeID = 13;
        else currentKnowledgeID = -999;
    }

}
