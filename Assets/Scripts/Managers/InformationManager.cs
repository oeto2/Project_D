using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
using Constants;
using Newtonsoft.Json;

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

    public void SaveInformation(Slot[] slots_)
    {
        for (int i = 0; i < slots_.Length; i++)
        {
            if (slots_[i].item != null)
            {
                saveLoadData.itemID[i] = slots_[i].item.id;
                saveLoadData.itemStack[i] = slots_[i].itemCount;
            }
            else
            {
                saveLoadData.itemID[i] = 0;
                saveLoadData.itemStack[i] = 0;
            }
        }
        SaveData();
    }

    public void SaveInformation(ItemType type, int itemID)
    {
        saveLoadData.equipmentItems[type] = itemID;
        SaveData();
    }

    public void SaveInformation(int gold_)
    {
        saveLoadData.gold += gold_;
        SaveData();
    }

    public void SaveData()
    {
        string jsonData = JsonConvert.SerializeObject(saveLoadData);
        File.WriteAllText(_path + _fileName, jsonData);
    }

    public void LoadData()
    {
        if (File.ReadAllText(_path + _fileName) != null)
        {
            string jsonData = File.ReadAllText(_path + _fileName);
            saveLoadData = JsonConvert.DeserializeObject<SaveLoadData>(jsonData);
        }
    }
}

[System.Serializable]
public class SaveLoadData
{
    // 창고, 인벤, 돈
    // 인벤토리 관련 정보
    public int[] itemID = new int[30];
    public int[] itemStack = new int[30];
    public int gold;

    // 장비창 관련 정보
    public Dictionary<ItemType, int> equipmentItems = new Dictionary<ItemType, int>()
    {
        {ItemType.Weapon, 0 },
        {ItemType.Equip, 0 }
    };

}