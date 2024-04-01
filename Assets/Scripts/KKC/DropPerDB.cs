using Constants;
using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DropPerDB : MonoBehaviour
{
    private Dictionary<int, DropPerData> _dropPer = new();

    [SerializeField] private List<ItemData> _commonItem = new List<ItemData>();
    [SerializeField] private List<ItemData> _uncommonItem = new List<ItemData>();
    [SerializeField] private List<ItemData> _rareItem = new List<ItemData>();
    [SerializeField] private List<ItemData> _uniqueItem = new List<ItemData>();

    System.Random random = new System.Random();

    private void Start()
    {
        ItemEnum();
        
        var res = Resources.Load<DropPerSheet>("DB/DropPerDBSheet");
        var dropPerSO = Object.Instantiate(res);
        var datas = dropPerSO.Drop_Table;

        if (datas == null || datas.Count <= 0)
            return;

        var dataCount = datas.Count;
        for (int i = 0; i < dataCount; i++)
        {
            var data = datas[i];

            if (_dropPer.ContainsKey(data.id))
                _dropPer[data.id] = data;
            else
                _dropPer.Add(data.id, data);
        }

        int rand = random.Next(1, Database.Monster.Get(11000001).monsterMaxRoot);
        for(int i = 0; i < rand; i++)
        {
            //GetItem(Database.Monster.Get(11000001).dropId);
            Debug.Log(GetItem(Database.Monster.Get(11000001).dropId).itemName);
        }
    }

    public void ItemEnum()
    {
        var eItem = Database.Item.DbEnumerator();
        while (eItem.MoveNext())
        {
            var item = eItem.Current;
            if (item is ItemData)
            {
                var data = (ItemData)item;

                switch (data.itemGrade)
                {
                    case ItemGrade.Common:
                        _commonItem.Add(data);
                        break;
                    case ItemGrade.Uncommon:
                        _uncommonItem.Add(data);
                        break;
                    case ItemGrade.Rare:
                        _rareItem.Add(data);
                        break;
                    case ItemGrade.Unique:
                        _uniqueItem.Add(data);
                        break;
                }
            }
        }
    }

    public ItemData GetItem(ItemGrade itemGrade_)
    {

        switch (itemGrade_)
        {
            case ItemGrade.Common:
                return _commonItem[random.Next(_commonItem.Count)];
            case ItemGrade.Uncommon:
                return _commonItem[random.Next(_uncommonItem.Count)];
            case ItemGrade.Rare:
                return _commonItem[random.Next(_rareItem.Count)];
            case ItemGrade.Unique:
                return _commonItem[random.Next(_uniqueItem.Count)];
        }
        return null;
    }

    public ItemData GetItem(int dropPerId_)
    {
        int rand = random.Next(_dropPer[dropPerId_].totalDropPer);
        
        if (rand <= _dropPer[dropPerId_].dropPer4)
        {
            return GetItem(ItemGrade.Unique);
        }
        else if (rand <= _dropPer[dropPerId_].dropPer3)
        {
            return GetItem(ItemGrade.Rare);
        }
        else if (rand <= _dropPer[dropPerId_].dropPer2)
        {
            return GetItem(ItemGrade.Uncommon);
        }
        else if (rand <= _dropPer[dropPerId_].dropPer1)
        {
            return GetItem(ItemGrade.Unique);
        }
        else
            return null;
    }
}