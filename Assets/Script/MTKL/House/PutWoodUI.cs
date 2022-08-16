using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PutWoodUI : MonoBehaviour
{
    [Header("檢測背包")]
    [SerializeField] private Inventory myBag;
    [SerializeField] private List<Item> woodListInBag;


    public void CheckWoodInBag()
    {
        foreach (var wood in woodListInBag)
        {
            if (myBag.itemList.Contains(wood))
            {

            }
        }
    }




}
