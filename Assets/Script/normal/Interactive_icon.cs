using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Interactive_icon : MonoBehaviour
{
    string myTag;
    string myName;
    GameObject mysSelf;
    public static Interactive_icon Instance;

    public void Awake()
    {
        myTag = this.tag;
        myName = this.name;
        Instance = this;
        mysSelf = this.gameObject;
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        InterectiveManager.Instance.openIcon(myTag);
        Debug.Log(myTag);
        InterectiveManager.Instance.WhichITouch(myName);
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        InterectiveManager.Instance.closeAllIcon();
    }

    public void DestroyGameObject()
    {
        Transform myParent = mysSelf.transform.parent;
        Destroy(myParent.gameObject);
    }

}
