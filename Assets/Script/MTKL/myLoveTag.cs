using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class myLoveTag : MonoBehaviour
{
    [SerializeField] public Inventory mybeg;

    bool netHave;

    // Start is called before the first frame update
    private void Awake()
    {
        netHave = false;
        this.transform.tag = "net";
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

        

    }


    public void IsNetInBag()
    {
        for (int i = 0; i < mybeg.itemList.Count; i++)
        {
            if (mybeg.itemList[i].itemID == 21)
            {
                netHave = true;
            }
            else
            {
                netHave = false;
            }
        }
    }



}