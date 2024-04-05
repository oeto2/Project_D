using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ShopData
{
    [SerializeField] private int _id;
    [SerializeField] private string _shopName;
    [SerializeField] private int _item1;
    [SerializeField] private int _item2;
    [SerializeField] private int _item3;
    [SerializeField] private int _item4;
    [SerializeField] private int _item5;

    public int id => _id;
    public string shopName => _shopName;

    private List<ShopItem> shopItemList;

    public List<ShopItem> ShopItemList
    {
        get
        {
            if(shopItemList == null)
            {
                shopItemList = new List<ShopItem>();

                CheckShopItem(_item1);
                CheckShopItem(_item2);
                CheckShopItem(_item3);
                CheckShopItem(_item4);
                CheckShopItem(_item5);
            }

            return shopItemList;
        }
    }

    private void CheckShopItem(int shopItemId_)
    {
        if(shopItemId_ != 0)
        {
            shopItemList.Add(new ShopItem(shopItemId_));
        }
    }
}

public class ShopItem
{
    public int item { get; }

    public ShopItem(int item_)
    {
        item = item_;
    }
}