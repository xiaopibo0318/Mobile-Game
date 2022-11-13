﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InterectiveManager : MonoBehaviour
{
    [SerializeField] IamInterective[] interectiveList;
    [SerializeField] GameObject interctiveBackground;
    public static InterectiveManager Instance;
    public GameObject nowTouch { get; set; }

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
        nowTouch = null;
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
        switch (myTouch)
        {
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
            case "northNotion":
                nextWay = 120;
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


            case "paperInterectiveTop":
                nextWay = 201;
                break;

            case "sandInterective150":
                nextWay = 211;
                break;
            case "sandInterective240":
                nextWay = 212;
                break;
            case "sandInterective400":
                nextWay = 213;
                break;
            case "treasureBoxInterective":
                nextWay = 221;
                break;

            case "interectiveLove":
                nextWay = 11;
                break;

            ///<summary>
            ///以下為宮殿的互動
            /// </summary>

            case "electricQuesInterective":
                nextWay = 31;
                break;
            case "scratchInterective":
                nextWay = 32;
                break;
            case "electricalBoxInterective":
                nextWay = 33;
                break;
            case "resistanceInterective":
                nextWay = 34;
                break;
            case "electricalBoxMotorInterective":
                nextWay = 35;
                break;
            case "raycastInterective":
                nextWay = 36;
                break;
            case "buzzerInterective":
                nextWay = 37;
                break;


            case "pillarInterectiveA":
                nextWay = 311;
                break;
            case "pillarInterectiveB":
                nextWay = 312;
                break;
            case "pillarInterectiveC":
                nextWay = 313;
                break;
            case "pillarInterectiveD":
                nextWay = 314;
                break;

            case "paperInterective1":
                nextWay = 321;
                break;
            case "paperInterective2":
                nextWay = 322;
                break;
            case "paperInterective7":
                nextWay = 327;
                break;

            case "paperInterective9":
                nextWay = 329;
                break;

            ///四個共用
            case "motorInterective":
                nextWay = 401;
                break;
            case "arduinoInterective":
                nextWay = 402;
                break;
            case "breadboardInterective":
                nextWay = 403;
                break;
            case "stringInterective":
                nextWay = 404;
                break;
            case "ledInterective":
                nextWay = 405;
                break;

            case "hiddenDoorInterective":
                nextWay = 411;
                break;

            case "storyInterective4":
                nextWay = 464;
                break;
            case "storyInterective5":
                nextWay = 465;
                break;
            case "storyInterective6":
                nextWay = 466;
                break;
            case "storyInterective7":
                nextWay = 467;
                break;
            case "storyInterective8":
                nextWay = 468;
                break;
            case "storyInterective9":
                nextWay = 469;
                break;
            case "storyInterective10":
                nextWay = 470;
                break;
            case "storyInterective11":
                nextWay = 471;
                break;
            case "storyInterective12":
                nextWay = 472;
                break;
            case "storyInterective13":
                nextWay = 473;
                break;
            case "storyInterective21":
                nextWay = 474;
                break;

            case "posterInterective1":
                nextWay = 481;
                break;
            case "posterInterective2":
                nextWay = 482;
                break;
            case "posterInterective3":
                nextWay = 483;
                break;
            case "posterInterective4":
                nextWay = 484;
                break;
            case "posterInterective5":
                nextWay = 485;
                break;
            case "posterInterective6":
                nextWay = 486;
                break;

            case "phoenixInterectiveGD":
                nextWay = 501;
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
                WiresawManager.Instance.ResetWireSaw();
                break;
            case 2:
                DialogueMTKL.Instance.StartDialogue();
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
                NotionManager.Instance.closeQues();
                break;
            case 110:
                CanvasManager.Instance.openCanvas("Notion");
                NotionManager.Instance.setText(nextWay);
                NotionManager.Instance.closeQues();
                break;
            case 120:
                CanvasManager.Instance.openCanvas("Notion");
                NotionManager.Instance.setText(nextWay);
                NotionManager.Instance.openQues();
                break;

            case 1111:
                InventoryManager.Instance.AddNewItem(myItemList[0]);
                WoodManager.Instance.DestroyNowWood();
                Player.Instance.PlayerCache();
                break;
            case 1112:
                InventoryManager.Instance.AddNewItem(myItemList[1]);
                Player.Instance.PlayerCache();
                break;
            case 1113:
                InventoryManager.Instance.AddNewItem(myItemList[2]);
                DestroyTouchObject();
                Player.Instance.PlayerCache();
                break;



            case 21:
                CanvasManager.Instance.openCanvas("ball");
                break;

            case 201:
                CanvasManager.Instance.openCanvas("sandPaper");
                BookContentManager.Instance.ActivateKnowledge(1);
                cacheVisable.Instance.siginalSomething("有關砂紙的知識已登入百科全書");
                break;

            case 211:
                InventoryManager.Instance.AddNewItem(myItemList[3]);
                break;
            case 212:
                InventoryManager.Instance.AddNewItem(myItemList[4]);
                break;
            case 213:
                InventoryManager.Instance.AddNewItem(myItemList[5]);
                break;
            case 221:
                //通關
                Player.Instance.myStatus.UpdateBookClick();
                GameCenter.Instance.EndGame();
                break;

            case 11:
                myLoveTag.Instance.CacheOrSignal();
                break;


            case 31:
                Debug.Log("AAA");
                CanvasManager.Instance.openCanvas("ElectricQues");
                break;
            case 32:
                CanvasManager.Instance.openCanvas("ScratchWalk");
                break;
            case 33:
                CanvasManager.Instance.openCanvas("LightElectric");
                break;
            case 34:
                CanvasManager.Instance.openCanvas("Resistance");
                break;
            case 35:
                CanvasManager.Instance.openCanvas("motor");
                break;
            case 36:
                CanvasManager.Instance.openCanvas("HandleRaycast");
                break;
            case 37:
                CanvasManager.Instance.openCanvas("BuzzerElectric");
                break;

            case 311:
                CanvasManager.Instance.openCanvas("FourPillar");
                FourPillarManager.Instance.nowQues = 0;
                FourPillarManager.Instance.TriggerQues();
                break;
            case 312:
                CanvasManager.Instance.openCanvas("FourPillar");
                FourPillarManager.Instance.nowQues = 1;
                FourPillarManager.Instance.TriggerQues();
                break;
            case 313:
                CanvasManager.Instance.openCanvas("FourPillar");
                FourPillarManager.Instance.nowQues = 2;
                FourPillarManager.Instance.TriggerQues();
                break;
            case 314:
                CanvasManager.Instance.openCanvas("FourPillar");
                FourPillarManager.Instance.nowQues = 3;
                FourPillarManager.Instance.TriggerQues();
                break;


            case 321:
                StoryManager.Instance.currentID = 0;
                StoryManager.Instance.SwitchPaper();
                CanvasManager.Instance.openCanvas("Bamboo");
                break;
            case 322:
                StoryManager.Instance.currentID = 1;
                StoryManager.Instance.SwitchPaper();
                CanvasManager.Instance.openCanvas("Bamboo");
                break;
            case 327:
                StoryManager.Instance.currentID = 6;
                StoryManager.Instance.SwitchPaper();
                CanvasManager.Instance.openCanvas("Bamboo");
                break;
            case 329:
                StoryManager.Instance.currentID = 8;
                StoryManager.Instance.SwitchPaper();
                CanvasManager.Instance.openCanvas("Bamboo");
                break;

            case 401: ///四個共用
                ItemUIManagerGD.Instance.AddItemToBag("馬達");
                DestroyTouchObject();
                break;
            case 402:
                ItemUIManagerGD.Instance.AddItemToBag("Arduino");
                DestroyTouchObject();
                break;
            case 403:
                ItemUIManagerGD.Instance.AddItemToBag("麵包版");
                break;
            case 404:
                ItemUIManagerGD.Instance.AddItemToBag("杜邦線");
                break;
            case 405:
                ItemUIManagerGD.Instance.AddItemToBag("LED");
                break;

            case 411:
                GDMananger.Instance.TriggerHiddenDoor();
                SiginalUI.Instance.SiginalText("好像哪裡傳來了一些動靜");
                break;

            case 464:
                StoryManager.Instance.currentID = 0;
                StoryManager.Instance.SwitchImage();
                CanvasManager.Instance.openCanvas("Bamboo");
                break;
            case 465:
                StoryManager.Instance.currentID = 1;
                StoryManager.Instance.SwitchImage();
                CanvasManager.Instance.openCanvas("Bamboo");
                break;
            case 466:
                StoryManager.Instance.currentID = 2;
                StoryManager.Instance.SwitchImage();
                CanvasManager.Instance.openCanvas("Bamboo");
                break;
            case 467:
                StoryManager.Instance.currentID = 3;
                StoryManager.Instance.SwitchImage();
                CanvasManager.Instance.openCanvas("Bamboo");
                break;
            case 468:
                StoryManager.Instance.currentID = 4;
                StoryManager.Instance.SwitchImage();
                CanvasManager.Instance.openCanvas("Bamboo");
                break;
            case 469:
                StoryManager.Instance.currentID = 5;
                StoryManager.Instance.SwitchImage();
                CanvasManager.Instance.openCanvas("Bamboo");
                break;
            case 470:
                StoryManager.Instance.currentID = 6;
                StoryManager.Instance.SwitchImage();
                CanvasManager.Instance.openCanvas("Bamboo");
                break;
            case 471:
                StoryManager.Instance.currentID = 7;
                StoryManager.Instance.SwitchImage();
                CanvasManager.Instance.openCanvas("Bamboo");
                break;
            case 472:
                StoryManager.Instance.currentID = 8;
                StoryManager.Instance.SwitchImage();
                CanvasManager.Instance.openCanvas("Bamboo");
                break;
            case 473:
                StoryManager.Instance.currentID = 9;
                StoryManager.Instance.SwitchImage();
                CanvasManager.Instance.openCanvas("Bamboo");
                break;
            case 474:
                StoryManager.Instance.currentID = 10;
                StoryManager.Instance.SwitchImage();
                CanvasManager.Instance.openCanvas("Bamboo");
                StartCoroutine(EndGame());
                break;

            case 481:
                StoryManager.Instance.currentID = 0;
                StoryManager.Instance.SwitchPoster();
                CanvasManager.Instance.openCanvas("Bamboo");
                break;
            case 482:
                StoryManager.Instance.currentID = 1;
                StoryManager.Instance.SwitchPoster();
                CanvasManager.Instance.openCanvas("Bamboo");
                break;
            case 483:
                StoryManager.Instance.currentID = 2;
                StoryManager.Instance.SwitchPoster();
                CanvasManager.Instance.openCanvas("Bamboo");
                break;
            case 484:
                StoryManager.Instance.currentID = 3;
                StoryManager.Instance.SwitchPoster();
                CanvasManager.Instance.openCanvas("Bamboo");
                break;
            case 485:
                StoryManager.Instance.currentID = 4;
                StoryManager.Instance.SwitchPoster();
                CanvasManager.Instance.openCanvas("Bamboo");
                break;
            case 486:
                StoryManager.Instance.currentID = 5;
                StoryManager.Instance.SwitchPoster();
                CanvasManager.Instance.openCanvas("Bamboo");
                break;
            case 501:
                DialogueManageGD.Instance.StartDialogue();
                CanvasManager.Instance.openCanvas("Phoenix");
                break;
        }
    }


    IEnumerator EndGame()
    {
        SiginalUI.Instance.SiginalText("恭喜破關\n將在20秒後傳送至結算畫面");
        float time = 20;
        while (time > 0)
        {
            yield return null;
            time -= Time.deltaTime;
        }
        Player.Instance.myStatus.UpdateBookClick();
        GameCenter.Instance.EndGame();
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


    //public void TakeWood()
    //{
    //    Wood.Instance.AddNewItem();
    //}

    private void DestroyTouchObject()
    {
        if (nowTouch != null)
        {
            Destroy(nowTouch);
        }
    }


}
