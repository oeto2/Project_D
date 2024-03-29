using Constants;
using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemEnumTest : MonoBehaviour
{
    [SerializeField] private List<ItemData> _commonItem = new List<ItemData>();
    [SerializeField] private List<ItemData> _uncommonItem = new List<ItemData>();
    [SerializeField] private List<ItemData> _rareItem = new List<ItemData>();
    [SerializeField] private List<ItemData> _uniqueItem = new List<ItemData>();

    private void Start()
    {
        ItemEnum();
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
        System.Random random = new System.Random();
        
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
}