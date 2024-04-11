using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopDB
{
    private Dictionary<int, ShopData> shopItems = new();

    public ShopDB()
    {
        var res = Resources.Load<ShopDBSheet>("DB/ShopDBSheet");
        var shopSO = Object.Instantiate(res);
        var entities = shopSO.Shop_Table;

        if (entities == null || entities.Count <= 0)
            return;

        var entityCount = entities.Count;
        for (int i = 0; i < entityCount; i++)
        {
            var shopItem = entities[i];

            if (shopItems.ContainsKey(shopItem.id))
                shopItems[shopItem.id] = shopItem;
            else
                shopItems.Add(shopItem.id, shopItem);
        }
    }

    public ShopData Get(int id)
    {
        if (shopItems.ContainsKey(id))
            return shopItems[id];

        return null;
    }
}
