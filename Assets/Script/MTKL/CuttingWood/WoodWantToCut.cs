using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class WoodWantToCut : MonoBehaviour
{
    public int woodID;
    public int slotID;
    Button button;
    CutWoodManager cutWoodManager;

    private void Awake()
    {
        //這邊有做更改成新的
        cutWoodManager = GameObject.Find("NewCuttingWood").GetComponent<CutWoodManager>();
        button = gameObject.GetComponent<Button>();
        button.onClick.AddListener(WoodOnClick);

    }


    public void WoodOnClick()
    {
        cutWoodManager.OpenWoodSimple(slotID);
    }

    public int getWoodID()
    {
        return woodID;
    }

}
