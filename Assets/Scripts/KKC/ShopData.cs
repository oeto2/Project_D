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
    [SerializeField] private int _item6;
    [SerializeField] private int _item7;
    [SerializeField] private int _item8;
    [SerializeField] private int _item9;
    [SerializeField] private int _item10;
    [SerializeField] private int _item11;
    [SerializeField] private int _item12;
    [SerializeField] private int _item13;
    [SerializeField] private int _item14;
    [SerializeField] private int _item15;
    [SerializeField] private int _item16;

    public int id => _id;
    public string shopName => _shopName;

    private List<int> shopItemList;

    public List<int> ShopItemList
    {
        get
        {
            if(shopItemList == null)
            {
                shopItemList = new List<int>();

                CheckShopItem(_item1);
                CheckShopItem(_item2);
                CheckShopItem(_item3);
                CheckShopItem(_item4);
                CheckShopItem(_item5);
                CheckShopItem(_item6);
                CheckShopItem(_item7);
                CheckShopItem(_item8);
                CheckShopItem(_item9);
                CheckShopItem(_item10);
                CheckShopItem(_item11);
                CheckShopItem(_item12);
                CheckShopItem(_item13);
                CheckShopItem(_item14);
                CheckShopItem(_item15);
                CheckShopItem(_item16);
            }

            return shopItemList;
        }
    }

    private void CheckShopItem(int shopItemId_)
    {
        if(shopItemId_ != 0)
        {
            shopItemList.Add(shopItemId_);
        }
    }
}