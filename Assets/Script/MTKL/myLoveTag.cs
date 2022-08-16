using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class myLoveTag : MonoBehaviour
{
    [SerializeField] private Inventory mybeg;

    [SerializeField] private SiginalUI signalUI;

    string myTag;
    string myName;
    bool netHave;

    // Start is called before the first frame update
    private void Awake()
    {
        myTag = this.tag;
        myName = this.name;
        netHave = false;
        this.transform.tag = "cache";

    }

    private void FixedUpdate()
    {
        if (netHave == false)
        {
            this.transform.tag = "cache";
        }else if (netHave == true)
        {
            this.transform.tag = "net";
        }


        myTag = this.tag;
    }


    public void IsNetInBag()
    {
        foreach(var myItem in mybeg.itemList)
        {
            if (myItem == null) continue;

            if (myItem.itemID == 21)
            {
                netHave = true;
                break;
            }
            else
            {
                netHave = false;
            }
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        IsNetInBag();
        myTag = this.tag;
        InterectiveManager.Instance.openIcon(myTag);
        Debug.Log(myTag);
        if(myTag != "net")
        {
            signalUI.SiginalText("水有點深，好像需要一些工具才能撈到");
        }
        else
        {
            InterectiveManager.Instance.WhichITouch(myName);
        }
        
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        InterectiveManager.Instance.closeAllIcon();
    }


}