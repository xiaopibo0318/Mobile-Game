using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance;

    public Inventory myBag;
    public GameObject slotGrid;
    public Slot slotPrefab;
    public GameObject emptySlot;
    public Text itemInformation;

    public List<GameObject> slots = new List<GameObject>();

    [Header("丟棄提示信息")]
    public GameObject siginalContent;
    public Text siginalText;
    public GameObject confirm;
    public GameObject dontDo;

    Item temp;

    public GameObject siginalObject;
    public SiginalUI siginalUI;
    
    private void Awake()
    {
        //siginalObject = GameObject.FindGameObjectWithTag("SiginalUI");
        //siginalUI = siginalObject.GetComponent<SiginalUI>();
        siginalContent.SetActive(false);

        if (Instance != null)
            Destroy(this);
        Instance = this;

    }

    private void OnEnable()
    {
    
        RefreshItem();
        //Instance.itemInformation.text = "";
    }

    public void UpdateItemInfo(string ItemDescription)
    {
        //itemInformation.text = ItemDescription;
    }

    
    /// <summary>
    /// 背包添加物品
    /// </summary>
    /// <param name="thisItem"></param>
    public void AddNewItem(Item thisItem)
    {
        if (!myBag.itemList.Contains(thisItem))
        {
            if (CheckBagFull())
            {
                return;
            }
            //myBag.itemList.Add(thisItem);
            //背包創建新物品
            //InventoryManager.CreateNewItem(thisItem);
            for (int i = 0; i < myBag.itemList.Count; i++)
            {
                if (myBag.itemList[i] == null)
                {
                    myBag.itemList[i] = thisItem;

                    //將Slot編號的物品改為這項東西
                    Instance.slots[i].GetComponent<Slot>().slotItem = thisItem;
                    thisItem.itemHave += 1;
                    break;
                }
            }
        }
        else
        {
            thisItem.itemHave += 1;
        }

        RefreshItem();
        cacheVisable.Instance.cacheSomething(thisItem);
    }


    public static void RefreshItem()
    {
        for (int i = 0; i < Instance.slotGrid.transform.childCount; i++)
        {
            //如果下方沒子物件，就不執行
            if (Instance.slotGrid.transform.childCount == 0)
                break;
            //摧毀所有子物件
            Destroy(Instance.slotGrid.transform.GetChild(i).gameObject);
            Instance.slots.Clear();
        }

        //判斷背包內有多少物品
        for(int i = 0; i < Instance.myBag.itemList.Count; i++)
        {
            //小於1的數量刪掉
            if (Instance.myBag.itemList[i] != null)
            {
                if (Instance.myBag.itemList[i].itemHave < 1)
                {
                    Instance.myBag.itemList[i] = null;
                }
            }
                
            //CreateNewItem(Instance.myBag.itemList[i]);

            //生成空格子
            Instance.slots.Add(Instantiate(Instance.emptySlot));
            Instance.slots[i].transform.SetParent(Instance.slotGrid.transform);
            Instance.slots[i].transform.localScale = new Vector3(1, 1, 1);

            //給ID值
            Instance.slots[i].GetComponent<Slot>().slotID = i;

            //將背包系統的物品掛到slot格子上
            Instance.slots[i].GetComponent<Slot>().slotItem = Instance.myBag.itemList[i];

            //把背包的物品給列表
            Instance.slots[i].GetComponent<Slot>().SetupSlot(Instance.myBag.itemList[i]);

            

        }

    }

    public void SubItem(Item thisItem,int num=1)
    {
        if (myBag.itemList.Contains(thisItem))
        {
            thisItem.itemHave -= num;
        }
        else
        {
            return;
        }

        RefreshItem();
    }


    public void SiginalText(Item item)
    {
        siginalContent.SetActive(true);
        siginalText.text = "確定要丟棄" + item.itemName;
        temp = item;
        confirm.SetActive(true);
        dontDo.SetActive(true);
    }

    public void CouldNotCrash()
    {
        siginalContent.SetActive(true);
        siginalText.text = "該物品不可丟棄";
        confirm.SetActive(false);
        dontDo.SetActive(false);
        StartCoroutine(signalDisable());
    }

    public void RemoveSomethong()
    {
        siginalContent.SetActive(false);
        SubItem(temp, 1);

    }

    public void CancelRemove()
    {
        siginalContent.SetActive(false);
    }

    IEnumerator signalDisable()
    {
        float myTime = 5;
        while (myTime > 0)
        {
            yield return new WaitForSeconds(1);
            myTime -= 1;
        }
        siginalContent.SetActive(false);
    }


    public bool isBagFull()
    {
        var Count = 0;
        for (int i = 0; i < 8; i++)
        {
            if (Instance.myBag.itemList[i] == null)
            {
                return false;
            }
            else
            {
                Count += 1;
            }
        }
        if (Count == 8) return true;
        else return false;
    }

    public bool CheckBagFull()
    {
        if (isBagFull())
        {
            siginalUI.SiginalText("背包已滿，請丟棄物品在撿取");
            return true;
        }
        return false;
    }

}