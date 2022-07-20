using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New block", menuName = "Scratch/New blockList")]
public class blockList : ScriptableObject
{
    public List<int> exeList = new List<int>();
}
