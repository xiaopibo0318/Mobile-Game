using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjecttoSave : MonoBehaviour, ISaveable
{

    public string PlayerName = "Howie";


    private void Start()
    {

    }

    public object SaveState()
    {
        return new SaveData()
        {
            PlayerName = this.PlayerName
        };
    }

    public void LoadState(object state)
    {
        var saveData = (SaveData)state;
        PlayerName = saveData.PlayerName;
    }




    [Serializable]
    private struct SaveData
    {
        public string PlayerName;
    }
}
