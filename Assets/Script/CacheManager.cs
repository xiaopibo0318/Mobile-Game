using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CacheManager : MonoBehaviour
{
    [SerializeField] GameObject[] itemlists;
    public static CacheManager Instance;

    private void Awake()
    {
        Instance = this;
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
