using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
using Constants;

public class InformationManager : SingletonBase<InformationManager>
{
    public SaveLoadData saveLoadData = new SaveLoadData();

    private string _path;
    private string _fileName = "SavePlayerData";

    private void Awake()
    {
        _path = Application.dataPath + "/";

        LoadData();
    }

    public void SaveInformation(int index, int id, int count = 1)
    {
        saveLoadData.itemID[index] = id;
        saveLoadData.itemStack[index] = count;
        SaveData();
    }

    public void SaveInformation(ItemType type, ItemData itemData_)
    {
        saveLoadData.equipmentItems[type] = itemData_;
        SaveData();
    }

    public void SaveInformation(int gold_)
    {
        saveLoadData.gold += gold_;
        SaveData();
    }

    public void SaveData()
    {
        string jsonData = JsonUtility.ToJson(saveLoadData);
        Debug.Log(_path + _fileName);
        File.WriteAllText(_path + _fileName, jsonData);
    }

    public void LoadData()
    {
        if (File.ReadAllText(_path + _fileName) != null)
        {
            string jsonData = File.ReadAllText(_path + _fileName);
            saveLoadData = JsonUtility.FromJson<SaveLoadData>(jsonData);
        }
    }
}

public class SaveLoadData
{
    // 창고, 인벤, 돈
    // 인벤토리 관련 정보
    public int[] itemID = new int[30];
    public int[] itemStack = new int[30];
    public int gold;

    // 장비창 관련 정보
    public Dictionary<ItemType, ItemData> equipmentItems = new Dictionary<ItemType, ItemData>()
    {
        {ItemType.Weapon, null },
        {ItemType.Equip, null }
    };
}