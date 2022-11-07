using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemUIManagerGD : Singleton<ItemUIManagerGD>
{
    [SerializeField] private List<Item> items = new List<Item>();
    [SerializeField] private Button confirmButton;
    [SerializeField] private Button cancelButton;
    public int currentID { get; set; }

    private void Start()
    {

    }

    
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

    private void CancelUse()
    {
        SiginalUI.Instance.SiginalText("不用就不用，哼");
    }

    private void ButtonTrigger() => UseItem(currentID);

    private void UseItem(int itemID)
    {
        switch (itemID)
        {
            case 499:

            default:
                break;
        }
    }

}
