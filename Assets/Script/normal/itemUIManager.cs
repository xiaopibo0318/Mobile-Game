using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class itemUIManager : MonoBehaviour
{

    [Header("背包系統")]
    public Inventory myBag;

    [Header("西王母的愛放置")]
    public GameObject lovePrefab;
    public GameObject player;
    public Item Love;

    [Header("木頭加工變木板")]
    public GameObject interectiveWood;
    public Item wood;
    public Item woodBoard;

    [Header("切割木頭")]
    public GameObject interectiveBoard;
    public Item wiresaw;
    bool isWiresaw;

    public void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void FixedUpdate()
    {
        if (MachineWithLove.Instance.GetIsElectric())
        {
            interectiveWood.SetActive(true);
        }
        else interectiveWood.SetActive(false);

        if (myBag.itemList.Contains(wiresaw))
        {
            interectiveBoard.SetActive(true);
        }
        else interectiveBoard.SetActive(false);
    }


    public void settingLove()
    {
        Instantiate(lovePrefab, new Vector3(player.transform.position.x, player.transform.position.y, player.transform.position.z), Quaternion.identity);
        InventoryManager.Instance.SubItem(Love,1);
    }

    public void CutWood()
    {
        InventoryManager.Instance.SubItem(wood, 1);
        InventoryManager.Instance.AddNewItem(woodBoard);
    }

    public void UsingWiresawToCut()
    {
        CanvasManager.Instance.openCanvas("cuttingWood");
    }
    

}
