using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class MenuItemTool 
{
    [MenuItem("CCMenuItem/OpenURL")]
    public static void OpenURL()
    {
        Application.OpenURL(Application.persistentDataPath);
    }
}
