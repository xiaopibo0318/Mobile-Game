using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardManager : MonoBehaviour
{
    public List<GameObject> myNews;
    public GameObject goBack;
    public GameObject backGround;


    public void Awake()
    {
        CloseBoard();
    }


    public void OpenNews(int n)
    {
        myNews[n].SetActive(true);
        goBack.SetActive(true);
        backGround.SetActive(true); 
    }


    public void CloseBoard()
    {
        for (int i = 0; i < myNews.Count; i++)
        {
            myNews[i].SetActive(false);
            goBack.SetActive(false);
            backGround.SetActive(false);
        }
    }

}
