using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Wood : MonoBehaviour
{
    
    public Item thisItem;
    public Inventory playerInventory;
    private bool isMe;
    string myTag;
    public int woodID;

    private void Awake()
    {
      
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
        Debug.Log("木頭ID" + woodID);
        WoodManager.Instance.GetNowWood(woodID);
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        Debug.Log("已經離開");
        //UIManager.Instance.CacheVisible(false);
        InterectiveManager.Instance.closeAllIcon();
        isMe = false;
    }

   


    
}
