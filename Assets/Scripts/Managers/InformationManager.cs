using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
using Constants;

public class InformationManager : SingletonBase<InformationManager>
{
    SaveLoadData saveLoadData = new SaveLoadData();

    private string _path;
    private string _fileName = "SavePlayerData";

    private void Awake()
    {
        _path = Application.dataPath + "/";

        LoadData();
    }

    public void SaveInformation(Slot[] slots_)
    {
        saveLoadData.slots = slots_;
        SaveData();
    }

    public void SaveInformation(ItemData itemData_)
    {
        saveLoadData.equipmentItems[itemData_.itemType] = itemData_;
        SaveData();
    }

    public void SaveInformation(int gold_)
    {
        saveLoadData.gold = gold_;
        SaveData();
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
    // 인벤토리 관련 정보
    public Slot[] slots;
    public int gold;

    // 장비창 관련 정보
    public Dictionary<ItemType, ItemData> equipmentItems = new Dictionary<ItemType, ItemData>()
    {
        {ItemType.Weapon, null },
        {ItemType.Equip, null }
    };
}