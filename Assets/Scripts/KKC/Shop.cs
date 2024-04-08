using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : UIRecycleViewController<ItemData>
{
    [field: SerializeField] public ShopListSO ShopItems;
    private List<int> ShopItemsList;
   
    private int _shopCode = 10000001;

    // 리스트 항목의 데이터를 읽어 들이는 메서드
    protected override void Start()
    {
        base.Start();

        LoadShopItemData();
        ShopItemSet();
    }

    private void LoadShopItemData()
    {
        var shopData = Database.Shop.Get(_shopCode);
        ShopItemsList = shopData.ShopItemList;
    }

    private void ShopItemSet()
    {
        tableData = new List<ItemData>();
        foreach (var shopItemId in ShopItemsList)
        {
            var shopItem = Database.Item.Get(shopItemId);
            tableData.Add(shopItem);
        }

        foreach (var item in tableData)
        {
            Debug.Log(item.id);
        }

        InitializeTableView();
    }
}
