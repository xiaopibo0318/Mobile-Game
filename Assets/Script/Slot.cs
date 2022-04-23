﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Slot : MonoBehaviour
{
    public Item slotItem;
    public Image slotImage;
    public TMP_Text slotNum;
    public string slotInfo;
    public string slotName;


    public GameObject itemInSlot;


    public void ItemOnClick()
    {
        InventoryManager.Instance.UpdateItemInfo(slotInfo);
    }

    public void SetupSlot(Item item)
    {
        
        if (item == null)
        {
            itemInSlot.SetActive(false);
            return;
        }

        slotImage.sprite = item.itemImage;
        slotNum.text = item.itemHave.ToString();
        slotInfo = item.itemInfo;
    }

}
