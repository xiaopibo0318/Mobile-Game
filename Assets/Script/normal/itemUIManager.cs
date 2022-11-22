using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class itemUIManager : MonoBehaviour
{

    [Header("背包系統")]
    public Inventory myBag;

    [Header("西王母的愛放置")]
    public GameObject lovePrefab;
    public GameObject player;
    public Item Love;

    [Header("木頭加工變木板")]
    public GameObject interectiveWood;
    public Item wood;
    public Item woodBoard;

    [Header("切割木頭")]
    public GameObject interectiveBoard;
    public Item wiresaw;
    bool isWiresaw;

    [Header("球")]
    public GameObject ball0;
    public GameObject ball1;
    public GameObject ball2;


    [Header("鑰匙")]
    public GameObject interectiveKey;
    public GameObject treasureBox;



    [Header("引用")]
    public static itemUIManager Instance;
    public void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        interectiveKey.SetActive(false);
        Instance = this;
    }

    private void FixedUpdate()
    {
        if (SceneManager.GetActiveScene().buildIndex == 2)
        {
            if (MachineWithLove.Instance.GetIsElectric())
            {
                interectiveWood.SetActive(true);
            }
            else interectiveWood.SetActive(false);
        }


        if (myBag.itemList.Contains(wiresaw))
        {
            interectiveBoard.SetActive(true);
        }
        else interectiveBoard.SetActive(false);
    }


    public void settingLove()
    {
        Instantiate(lovePrefab, new Vector3(player.transform.position.x, player.transform.position.y, player.transform.position.z), Quaternion.identity);
        InventoryManager.Instance.SubItem(Love, 1);
        CanvasManager.Instance.openCanvas("original");
    }

    public void CutWood()
    {
        InventoryManager.Instance.SubItem(wood, 1);
        InventoryManager.Instance.AddNewItem(woodBoard);


    }

    public void UsingWiresawToCut()
    {
        CanvasManager.Instance.openCanvas("cuttingWood");
    }

    public string GetWhichBall()
    {
        if (ball0.activeInHierarchy)
        {
            return ball0.tag;
        }
        else if (ball1.activeInHierarchy)
        {
            return ball1.tag;
        }
        else if (ball2.activeInHierarchy)
        {
            return ball2.tag;
        }
        else { return null; }
    }

    public void ballCracker()
    {
        ///這個一定要先Update
        SandPaperManager.Instance.UpdateNowBallType(GetWhichBall());
        CanvasManager.Instance.openCanvas("SandPaperBall");

    }


    public void KeyUsable(bool inZone)
    {
        if (inZone)
            interectiveKey.SetActive(true);
        else
            interectiveKey.SetActive(false);
    }

    public void openBox()
    {
        cacheVisable.Instance.siginalSomething("遠處好像有一些聲音，某處可能有了變化");
        treasureBox.SetActive(true);
    }


}
