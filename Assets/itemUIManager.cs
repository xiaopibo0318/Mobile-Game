using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class itemUIManager : MonoBehaviour
{
    [Header("西王母的愛放置")]
    public GameObject lovePrefab;
    public GameObject player;
    public Item Love;

    public void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

  
    public void settingLove()
    {
        Instantiate(lovePrefab, new Vector3(player.transform.position.x, player.transform.position.y, player.transform.position.z), Quaternion.identity);
        InventoryManager.Instance.SubItem(Love,1);
    }


}
