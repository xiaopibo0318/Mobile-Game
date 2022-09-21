using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BookContentManager : MonoBehaviour
{
    [Header("已經激活之知識")]
    public List<GameObject> myKnowledge = new List<GameObject>();
    string nowKnowledge;

    [Header("所有知識")]
    public List<GameObject> allKnwledge = new List<GameObject>();

    [Header("知識存放處")]
    private Sprite[] SandPaperKnowledge;
    private Sprite[] WireSawKnowledge;
    //List<Sprite> SandPaperKnowledge = new List<Sprite>();

    [Header("頁面顯示")]
    public Image ImgRight;
    public Image ImgLeft;
    int currentID;
    public GameObject menu;
    bool nextPageFirst;

    public static BookContentManager Instance;

    private void Awake()
    {
        SandPaperKnowledge = Resources.LoadAll<Sprite>("Book/SandPaper");
        WireSawKnowledge = Resources.LoadAll<Sprite>("Book/WireSaw");

        currentID = 0;

        menu.SetActive(true);

        ImgLeft.color = new Color(255, 255, 255, 0);
        ImgRight.color = new Color(255, 255, 255, 0);

        for (int i = 0; i < myKnowledge.Count; i++)
        {
            myKnowledge[i].SetActive(false);
            allKnwledge[i].SetActive(false);
        }
        Instance = this;


    }

    public void Onclick(string name)
    {
        for (int i = 0; i < myKnowledge.Count; i++)
        {
            if (name == myKnowledge[i].name)
            {
                myKnowledge[i].SetActive(true);
                nowKnowledge = myKnowledge[i].name;

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

        if (nowKnowledge.Contains("sandpaper"))
        {
            if (currentID < SandPaperKnowledge.Length - 1)
            {
                ImgLeft.sprite = SandPaperKnowledge[currentID];
                ImgRight.sprite = SandPaperKnowledge[currentID + 1];
                currentID += 2;
                nextPageFirst = true;
            }
            if (currentID < 0 || currentID > SandPaperKnowledge.Length - 1)
            {
                currentID = 0;
                ImgLeft.color = new Color(255, 255, 255, 0f);
                ImgRight.color = new Color(255, 255, 255, 0f);
                nextPageFirst = false;

                menu.SetActive(true);

            }
        }
        else if (nowKnowledge.Contains("wiresaw"))
        {
            if (currentID < WireSawKnowledge.Length - 1)
            {
                ImgLeft.sprite = WireSawKnowledge[currentID];
                ImgRight.sprite = WireSawKnowledge[currentID + 1];
                currentID += 2;
                nextPageFirst = true;
            }
            if (currentID < 0 || currentID > WireSawKnowledge.Length - 1)
            {
                currentID = 0;
                ImgLeft.color = new Color(255, 255, 255, 0f);
                ImgRight.color = new Color(255, 255, 255, 0f);
                nextPageFirst = false;

                menu.SetActive(true);

            }
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



        if (nowKnowledge.Contains("sandpaper"))
        {
            if (currentID > -1)
            {
                ImgLeft.sprite = SandPaperKnowledge[currentID];
                ImgRight.sprite = SandPaperKnowledge[currentID + 1];
                if (nextPageFirst)
                    currentID -= 4;
                else
                    currentID -= 2;
            }
            if (currentID < 0 || currentID > SandPaperKnowledge.Length - 1)
            {
                currentID = 0;
                ImgLeft.color = new Color(255, 255, 255, 0f);
                ImgRight.color = new Color(255, 255, 255, 0f);
                menu.SetActive(true);

            }
        }
        else if (nowKnowledge.Contains("wiresaw"))
        {
            if (currentID > -1)
            {
                ImgLeft.sprite = WireSawKnowledge[currentID];
                ImgRight.sprite = WireSawKnowledge[currentID + 1];
                if (nextPageFirst)
                    currentID -= 4;
                else
                    currentID -= 2;
            }
            if (currentID < 0 || currentID > WireSawKnowledge.Length - 1)
            {
                currentID = 0;
                ImgLeft.color = new Color(255, 255, 255, 0f);
                ImgRight.color = new Color(255, 255, 255, 0f);
                menu.SetActive(true);

            }
        }
        //用過一次返回記得退掉。
        nextPageFirst = false;
    }


}
