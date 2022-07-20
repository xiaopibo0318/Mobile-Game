using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New block", menuName = "Scratch/New block")]
public class Block : ScriptableObject
{
    public int blockID;
    public string blockName;
    public Sprite blockImage;
}
