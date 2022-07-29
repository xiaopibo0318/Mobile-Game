using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PutWoodManager : MonoBehaviour
{
    [Header("背包判定")]
    public Inventory myBag;

    [Header("背包是否有此種木頭")]
    bool is301;
    bool is311;
    bool is321;
    bool is331;
    bool is341;
    bool is351;
    bool is361;
    bool is371;

    public void Awake()
    {
        is301 = false;
        is311 = false;
        is321 = false;
        is331 = false;
        is341 = false;
        is351 = false;
        is361 = false;
        is371 = false;
    }

    public void IsWoodInBag()
    {
        for (int i =0; i< myBag.itemList.Count; i++)
        { 
            switch (myBag.itemList[i].itemID){
                case 301:
                    is301 = true;
                    break;
                case 311:
                    is311 = true;
                    break;
                case 321:
                    is321 = true;
                    break;
                case 331:
                    is331 = true;
                    break;
                case 341:
                    is341 = true;
                    break;
                case 351:
                    is351 = true;
                    break;
                case 361:
                    is361 = true;
                    break;
                case 371:
                    is371 = true;
                    break;

            }
            
        }
    }

}
