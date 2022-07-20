using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CacheManager : MonoBehaviour
{
    [SerializeField] IamInterective[] interectiveList;
    public static CacheManager Instance;

    private void Awake()
    {
        Instance = this;
    }

    public void openIcon(string interectName)
    {
        //為了讓系統讀取到Menu列表，GUI上記得掛上去menu
        for (int i = 0; i < interectiveList.Length; i++)
        {
            if (interectiveList[i].interectName == interectName)
            {
                OpenIcon(interectiveList[i]);
            }
            else if (interectiveList[i].open)
            {
                CloseIcon(interectiveList[i]);
            }
        }
    }


    public void OpenIcon(IamInterective iconName)
    {
        for (int i = 0; i < interectiveList.Length; i++)
        {
            if (interectiveList[i].open)
            {
                CloseIcon(interectiveList[i]);
            }
        }
        iconName.Open();
    }

    public void CloseIcon(IamInterective iconName)
    {
        iconName.Close();
    }


    void WhichItemITouch(string itemName)
    {
        if(itemName == "Wood")
        {
            Wood.Instance.AddNewItem();
        }
    }

    public void TakeWood()
    {
        Wood.Instance.AddNewItem();
    }
    
}
