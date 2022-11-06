using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Interactive_icon : MonoBehaviour
{
    string myTag;
    string myName;
    GameObject mysSelf;

    public void Awake()
    {
        myTag = this.tag;
        myName = this.name;

        mysSelf = this.gameObject;

    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        InterectiveManager.Instance.openIcon(myTag);
        Debug.Log(myTag);
        InterectiveManager.Instance.WhichITouch(myName);
        InterectiveManager.Instance.nowTouch = mysSelf.transform.parent.gameObject;
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        InterectiveManager.Instance.closeAllIcon();
    }

}
