using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class InformationManager : SingletonBase<InformationManager>
{
    SaveLoadData saveLoadData = new SaveLoadData();

    private string _path;
    private string _fileName = "SavePlayerData";

    // 인벤토리 관련 정보
    public Slot[] slots;
    public int gold;
    // 장비창 관련 정보
    public ItemData weaponSlot;
    public ItemData equipSlot;

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
    // 창고, 인벤, 돈
}