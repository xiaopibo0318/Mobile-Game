using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Wood : MonoBehaviour
{
    [SerializeField] float lifetime;
    public Item thisItem;
    public Inventory playerInventory;
    public static Wood Instance;

    string myTag; 

    private void Awake()
    {
        Instance = this;
        myTag = this.tag;
    }

    void Update()
    {
        Destroy(gameObject, lifetime);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("已經碰觸");
        //UIManager.Instance.CacheVisible(true);
        InterectiveManager.Instance.openIcon(myTag);
        Debug.Log("木頭的tag:" + myTag);
        InterectiveManager.Instance.WhichITouch(this.name);
        Debug.Log(this.name);
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        Debug.Log("已經離開");
        //UIManager.Instance.CacheVisible(false);
        InterectiveManager.Instance.closeAllIcon();
    }

   

    
    public void AddNewItem()
    {
        if (!playerInventory.itemList.Contains(thisItem))
        {
            //playerInventory.itemList.Add(thisItem);
            //背包創建新物品
            //InventoryManager.CreateNewItem(thisItem);
            for(int i =0; i < playerInventory.itemList.Count; i++)
            {
                if(playerInventory.itemList[i] == null)
                {
                    playerInventory.itemList[i] = thisItem;
                    break;
                }
            }
        }
        else
        {
            thisItem.itemHave += 1;
        }

        InventoryManager.RefreshItem();

        //gameObject.SetActive(false);
        Destroy(gameObject);
        
    }
    
}
