using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class blockSlot : MonoBehaviour
{
    public int slotID;
    public Image slotImage;



    public GameObject blockInSlot;

    public void SetupSlot(Block block)
    {

        if (block == null)
        {
            blockInSlot.SetActive(false);
            return;
        }

        slotImage.sprite = block.blockImage;
    }
}
