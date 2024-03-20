using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class InformationManager : SingletonBase<InformationManager>
{
    SaveLoadData saveLoadData = new SaveLoadData();

    private string _path;
    private string _fileName = "SavePlayerData";

    private void Awake()
    {
        _path = Application.dataPath + "/";
    }

    public void SaveData()
    {
        string jsonData = JsonUtility.ToJson(saveLoadData);
        File.WriteAllText(_path + _fileName, jsonData);
    }

    public void LoadData()
    {
        string jsonData = File.ReadAllText(_path + _fileName);
        saveLoadData = JsonUtility.FromJson<SaveLoadData>(jsonData);
    }
}

class SaveLoadData
{
    // √¢∞Ì, ¿Œ∫•, µ∑
}