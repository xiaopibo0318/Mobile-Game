using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance;

    public Inventory myBag;
    public GameObject slotGrid;
    //public Slot slotPrefab;
    public GameObject emptySlot;
    public TMP_Text itemInformation;

    public List<GameObject> slots = new List<GameObject>();

    private void Awake()
    {
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
        ///itemInformation.text = ItemDescription;
    }

    /*
    public static void CreateNewItem(Item item)
    {
        //生成(東西、位置、角度)
        Slot newItem = Instantiate(Instance.slotPrefab, Instance.slotGrid.transform.position, Quaternion.identity);
        //設定其位置到slotGrid的子集
        newItem.gameObject.transform.SetParent(Instance.slotGrid.transform);
        //設定其Item大小
        newItem.transform.localScale = new Vector3(1, 1, 1);
        
        //設定這個newItem的信息
        newItem.slotItem = item;
        newItem.slotImage.sprite = item.itemImage;
        newItem.slotNum.text = item.itemHave.ToString();
    }
    */


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
            //CreateNewItem(Instance.myBag.itemList[i]);

            //生成空格子
            Instance.slots.Add(Instantiate(Instance.emptySlot));
            Instance.slots[i].transform.SetParent(Instance.slotGrid.transform);
            Instance.slots[i].transform.localScale = new Vector3(1, 1, 1);

            //把背包的物品給列表
            Instance.slots[i].GetComponent<Slot>().SetupSlot(Instance.myBag.itemList[i]);
        }

    }
}