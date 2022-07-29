using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetManager : MonoBehaviour
{
    [SerializeField]Item myLove;
    private void Awake()
    {
        
    }

    public void getLove()
    {
        InventoryManager.Instance.AddNewItem(myLove);
    }
}
