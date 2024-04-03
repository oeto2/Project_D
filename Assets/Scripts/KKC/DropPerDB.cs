using Constants;
using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[System.Serializable]
public class DropPerDB
{
    private Dictionary<int, DropPerData> _dropPer = new();

    private List<ItemData> _commonItem = new List<ItemData>();
    private List<ItemData> _uncommonItem = new List<ItemData>();
    private List<ItemData> _rareItem = new List<ItemData>();
    private List<ItemData> _uniqueItem = new List<ItemData>();

    private List<ItemGrade> _totalItem = new List<ItemGrade>();
    private List<int> _totalDropPer = new List<int>();

    System.Random random = new System.Random();

    public DropPerDB()
    {
        var res = Resources.Load<DropPerSheet>("DB/DropPerSheet");
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
                return _uncommonItem[random.Next(_uncommonItem.Count)];
            case ItemGrade.Rare:
                return _rareItem[random.Next(_rareItem.Count)];
            case ItemGrade.Unique:
                return _uniqueItem[random.Next(_uniqueItem.Count)];
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
        else if (rand <= _dropPer[dropPerId_].dropPer4 + _dropPer[dropPerId_].dropPer3)
        {
            return GetItem(ItemGrade.Rare);
        }
        else if (rand <= _dropPer[dropPerId_].dropPer4 + _dropPer[dropPerId_].dropPer3 + _dropPer[dropPerId_].dropPer2)
        {
            return GetItem(ItemGrade.Uncommon);
        }
        else if (rand <= _dropPer[dropPerId_].dropPer4 + _dropPer[dropPerId_].dropPer3 + _dropPer[dropPerId_].dropPer2 + _dropPer[dropPerId_].dropPer1)
        {
            return GetItem(ItemGrade.Common);
        }
        else
            return null;
    }
}