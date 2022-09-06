using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjecttoSave : MonoBehaviour, ISaveable
{
    [Header("場景物件")]
    //[SerializeField] private List<GameObject> gameObjects = new List<GameObject>();
    public GameObject[] ObjectsToSave;
    //public int a;

    private void Start()
    {
        //GameObject[] ObjectsToSave = new GameObject[gameObjects.Count];
    }

    public object SaveState()
    {
        return new SaveData()
        {
            ObjectsToSave = this.ObjectsToSave
        };
    }

    public void LoadState(object state)
    {
        var saveData = (SaveData)state;
        ObjectsToSave = saveData.ObjectsToSave;
    }




    [Serializable]
    private struct SaveData
    {
        public GameObject[] ObjectsToSave;
        //private List<GameObject> gameObjects;
        //public int a;
    }
}
