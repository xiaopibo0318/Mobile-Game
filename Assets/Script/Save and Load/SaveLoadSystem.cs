﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class SaveLoadSystem : MonoBehaviour
{
    public static SaveLoadSystem Instance;
    void Start()
    {
        Instance = this;
    }


    public string SavePath => $"{Application.persistentDataPath}/save.txt";

    //[ContextMenu("Save")]
    public void Save()
    {
        var state = LoadFile();
        SaveState(state);
        SaveFile(state);
        Debug.Log("存檔成功");
    }

    //[ContextMenu("Load")]
    public void Load()
    {
        var state = LoadFile();
        LoadState(state);
        KLMTmanager.Instance.KLMTinitial();
        Debug.Log(Player.Instance.myStatus.name);
        Debug.Log(Player.Instance.myStatus.emailAddress);
        Debug.Log(Player.Instance.myStatus.totalTime);
        Debug.Log(Player.Instance.myStatus.gameStatus);
    }


    public void SaveFile(object state)
    {
        using (var stream = File.Open(SavePath, FileMode.Create))
        {
            var formatter = new BinaryFormatter();
            formatter.Serialize(stream, state);
        }
    }

    Dictionary<string,object> LoadFile()
    {
        if (!File.Exists(SavePath))
        {
            Debug.Log("No save file found");
            return new Dictionary<string, object>();
        }

        using(FileStream stream = File.Open(SavePath,FileMode.Open))
        {
            var formatter = new BinaryFormatter();
            return (Dictionary<string, object>)formatter.Deserialize(stream);
        }
    }

    void SaveState(Dictionary<string, object> state)
    {
        foreach (var saveable in FindObjectsOfType<SaveableEntity>())
        {
            state[saveable.Id] = saveable.SaveState();
        }
    }

    void LoadState(Dictionary<string, object> state)
    {
        foreach (var saveable in FindObjectsOfType<SaveableEntity>())
        {
            if (state.TryGetValue(saveable.Id, out object savedState))
            {
                saveable.LoadState(savedState);
            }
        }
    }
}
