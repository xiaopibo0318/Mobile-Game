using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Wood : MonoBehaviour
{
    string myTag;
    public int woodID;

    private void Awake()
    {
      
        myTag = this.tag;
    }

    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        //UIManager.Instance.CacheVisible(true);
        InterectiveManager.Instance.openIcon(myTag);
        //Debug.Log("木頭的tag:" + myTag);
        InterectiveManager.Instance.WhichITouch(this.name);
        //Debug.Log(this.name);
        //Debug.Log("木頭ID" + woodID);
        WoodManager.Instance.ChangeNowWood(woodID);
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        //UIManager.Instance.CacheVisible(false);
        InterectiveManager.Instance.closeAllIcon();
    }

   


    
}
