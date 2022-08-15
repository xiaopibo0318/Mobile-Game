using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Wood : MonoBehaviour
{
    
    public Item thisItem;
    public Inventory playerInventory;
    public static Wood Instance;
    private bool isMe;
    string myTag; 

    private void Awake()
    {
        Instance = this;
        myTag = this.tag;
        isMe = false;
    }

    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("已經碰觸");
        //UIManager.Instance.CacheVisible(true);
        InterectiveManager.Instance.openIcon(myTag);
        Debug.Log("木頭的tag:" + myTag);
        InterectiveManager.Instance.WhichITouch(this.name);
        Debug.Log(this.name);
        isMe = true;
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        Debug.Log("已經離開");
        //UIManager.Instance.CacheVisible(false);
        InterectiveManager.Instance.closeAllIcon();
        isMe = false;
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
        if(isMe) Destroy(this.gameObject);
        
    }
    
}
