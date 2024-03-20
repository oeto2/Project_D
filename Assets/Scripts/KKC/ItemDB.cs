using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDB
{
    private Dictionary<int, ItemData> items = new();

    public ItemDB()
    {
        var res = Resources.Load<ItemDBSheet>("DB/ItemDBSheet");
        var itemSO = Object.Instantiate(res);
        var entities = itemSO.Item_Table;

        if (entities == null || entities.Count <= 0)
            return;

        var entityCount = entities.Count;
        for(int i = 0; i < entityCount; i++)
        {
            var item = entities[i];

            if (items.ContainsKey(item.id))
                items[item.id] = item;
            else
                items.Add(item.id, item);
        }
    }

    public ItemData Get(int id)
    {
        if (items.ContainsKey(id))
            return items[id];

        return null;
    }

    public IEnumerator DbEnumerator()
    {
        return items.GetEnumerator();
    }
}
