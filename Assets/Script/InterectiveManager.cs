using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InterectiveManager : MonoBehaviour
{
    [SerializeField] IamInterective[] interectiveList;
    [SerializeField] GameObject interctiveBackground;
    public static InterectiveManager Instance;

    int nextWay;

    string itemName;

    [Header("撿取物品")]
    [SerializeField] Item[] myItemList;

    private void Awake()
    {
        Instance = this;
        closeAllIcon();
        interctiveBackground.SetActive(false);
        nextWay = 0;
    }

    public void openIcon(string _interectName)
    {
        //為了讓系統讀取到Menu列表，GUI上記得掛上去menu
        for (int i = 0; i < interectiveList.Length; i++)
        {
            if (interectiveList[i].interectName == _interectName)
            {
                OpenIcon(interectiveList[i]);
            }
            else if (interectiveList[i].open)
            {
                CloseIcon(interectiveList[i]);
            }
        }
    }

    public void closeAllIcon()
    {
        for (int i = 0; i < interectiveList.Length; i++) 
        {
            CloseIcon(interectiveList[i]);
        }
        interctiveBackground.SetActive(false);
    }


    public void OpenIcon(IamInterective iconName)
    {
        for (int i = 0; i < interectiveList.Length; i++)
        {
            if (interectiveList[i].open)
            {
                CloseIcon(interectiveList[i]);
            }
        }
        iconName.Open();
        interctiveBackground.SetActive(true);
    }

    public void CloseIcon(IamInterective iconName)
    {
        iconName.Close();
        interctiveBackground.SetActive(false);
    }


    /// <summary>
    /// 藉由判斷碰觸的zone名稱，來決定下一個介面為何。
    /// </summary>
    /// <param name="myTouch"></param>

    public void WhichITouch(string myTouch)
    {
        switch(myTouch){
            case "wiresawInterective":
                nextWay = 1;
                break;
            case "phoenixInterective":
                nextWay = 2;
                break;
            case "houseInterective":
                nextWay = 3;
                break;
            case "flowerInterective":
                nextWay = 4;
                break;
            case "boardInterective":
                nextWay = 5;
                break;
            //case "machineInterective":
            //    nextWay = 6;
            //    break;
            case "stoneInterective":
                nextWay = 7;
                break;


            case "machineNotion":
                nextWay = 100;
                break;
            case "teleportNotion":
                nextWay = 110;
                break;


            case "WoodOnGround(Clone)":
                nextWay = 1111;
                break;
            case "findNetInterective":
                nextWay = 1112;
                break;
            case "loveInterective":
                nextWay = 1113;
                break;

            case "ballInterective":
                nextWay = 21;
                break;
        }

 
    }

    /// <summary>
    /// 用按鈕開始進行。
    /// </summary>
    public void nextPage()
    {
        switch (nextWay)
        {
            case 1:
                CanvasManager.Instance.openCanvas("Wiresaw");
                break;
            case 2:
                CanvasManager.Instance.openCanvas("Phoenix");
                break;
            case 3:
                CanvasManager.Instance.openCanvas("House");
                break;
            case 4:
                CanvasManager.Instance.openCanvas("Flower");
                break;
            case 5:
                CanvasManager.Instance.openCanvas("Board");
                break;
            //case 6:
            //    CanvasManager.Instance.openCanvas("Notion");
            //    break;
            case 7:
                CanvasManager.Instance.openCanvas("Stone");
                break;

            case 100:
                CanvasManager.Instance.openCanvas("Notion");
                NotionManager.Instance.setText(nextWay);
                break;
            case 110:
                CanvasManager.Instance.openCanvas("Notion");
                NotionManager.Instance.setText(nextWay);
                break;

            case 1111:
                Wood.Instance.AddNewItem();
                Player.Instance.PlayerCache();
                break;
            case 1112:
                InventoryManager.Instance.AddNewItem(myItemList[1]);
                Player.Instance.PlayerCache();
                break;
            case 1113:
                InventoryManager.Instance.AddNewItem(myItemList[2]);
                Interactive_icon.Instance.DestroyGameObject();
                Player.Instance.PlayerCache();
                break;



            case 21:
                CanvasManager.Instance.openCanvas("ball");
                break;
        }
    }


    //void WhichItemITouch(string itemName)
    //{
    //    if(itemName == "Wood")
    //    {
    //        Wood.Instance.AddNewItem();
    //    }
    //}


    public void whichICache()
    {
        switch (itemName)
        {
            case "net":
                break;
        }
    }


    public void TakeWood()
    {
        Wood.Instance.AddNewItem();
    }

}
