using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
using Constants;
using Newtonsoft.Json;
using UnityEngine.UIElements.Experimental;
using System.Net.NetworkInformation;
using Unity.VisualScripting;
using UnityEditor;

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
        //�κ��丮 ������ ����
        if (slots_.Length < 50)
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
        }

        //â�� ������ ����
        else
        {
            for (int i = 0; i < slots_.Length; i++)
            {
                if (slots_[i].item != null)
                {
                    saveLoadData.storage_ItemID[i] = slots_[i].item.id;
                    saveLoadData.storage_ItemStack[i] = slots_[i].itemCount;
                }
                else
                {
                    saveLoadData.storage_ItemID[i] = 0;
                    saveLoadData.storage_ItemStack[i] = 0;
                }
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
    // â��, �κ�, ��
    // �κ��丮 ���� ����
    public int[] itemID = new int[Constant.InvenSize];
    public int[] itemStack = new int[Constant.InvenSize];
    public int gold;

    //â�� ������
    public int[] storage_ItemID = new int[Constant.StorageSize];
    public int[] storage_ItemStack = new int[Constant.StorageSize];
    public int storage_Gold;

    // ���â ���� ����
    public Dictionary<ItemType, int> equipmentItems = new Dictionary<ItemType, int>()
    {
        {ItemType.Weapon, 0 },
        {ItemType.Helmet, 0 },
        {ItemType.Chest, 0 },
        {ItemType.Pants, 0 },
        {ItemType.Boots, 0 },
        {ItemType.Ring, 0 },
        {ItemType.Necklace, 0 }
    };
}