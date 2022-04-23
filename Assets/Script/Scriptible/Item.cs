using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/New Item")] 
public class Item : ScriptableObject
{
    public string itemName;
    public Sprite itemImage;
    public string itemTag;
    public int itemHave;
    [TextArea]
    public string itemInfo;

    public bool missioable;
    public bool storyable;


}
