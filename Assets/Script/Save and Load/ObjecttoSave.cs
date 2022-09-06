using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjecttoSave : MonoBehaviour// ,ISaveable
{
    [Header("場景物件")]
    [SerializeField] private List<GameObject> gameObjects = new List<GameObject>();
    
   /* public object SaveState()
    {
        /*return new SaveData()
        {

        }
    }*/

    public void LoadState(object state)
    {
       
    }

    private struct SaveData
    {
        /*for (int i = 0; i<gameObjects.Count; i++)
        {
            public GameObject gameObjects[i];
        }*/
        
    }
}
