using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemUIManagerGD : Singleton<ItemUIManagerGD>
{
    [SerializeField] private List<Item> items = new List<Item>();
    [SerializeField] private Button confirmButton;
    [SerializeField] private Button cancelButton;
    [SerializeField] private Button goBackButton;
    [SerializeField] private GameObject interectiveObject;
    [SerializeField] private Text mainInterectiveText;
    public int currentID { get; set; }

    private void Start()
    {
        goBackButton.onClick.AddListener(delegate { CanvasManager.Instance.openCanvas("original"); });
        confirmButton.onClick.AddListener(ButtonTrigger);
        cancelButton.onClick.AddListener(CancelUse);
        interectiveObject.SetActive(false);
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

    public void ChangeInterectiveName(int itemID)
    {
        for (int i = 0; i < items.Count; i++)
        {
            if (items[i].itemID == itemID)
            {
                mainInterectiveText.text = "是否要使用\n" + items[i].name;
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
                CanvasManager.Instance.openCanvas("Coding");
                break;
            default:
                SiginalUI.Instance.SiginalText("這無法使用呦！");
                break;
        }
    }

}
