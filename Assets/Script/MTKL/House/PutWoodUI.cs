using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PutWoodUI : MonoBehaviour
{
    [Header("檢測背包")]
    [SerializeField] private Inventory myBag;
    [SerializeField] private List<Item> woodListInBag;

    public static PutWoodUI Instance;

    private void Awake()
    {
        Instance = this;
    }

    public bool CheckWoodInBag(int nowWoodID)
    {
        Debug.Log("現在選擇的類型是：" + nowWoodID);
        foreach (var wood in woodListInBag)
        {
            //如果沒東西就跳過，
            if (!myBag.itemList.Contains(wood)) continue;

            //有的話 去回傳值給Shape去做判斷
            if (nowWoodID == wood.itemID)
            {
                return true;
            }
        }
        return false;
    }




}
