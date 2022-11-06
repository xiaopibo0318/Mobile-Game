using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemUIManagerGD : Singleton<ItemUIManagerGD>
{
    [SerializeField] private List<Item> items = new List<Item>();


    public void AddItemToBag(string itemName)
    {
        for (int i = 0; i < items.Count; i++)
        {
            if (items[i].itemName == itemName)
            {
                InventoryManager.Instance.AddNewItem(items[i]);
            }
        }
    }


}
