using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : UIRecycleViewController<ItemData>
{
    [field: SerializeField] public ShopListSO ShopItems;

    // ����Ʈ �׸��� �����͸� �о� ���̴� �޼���
    protected override void Start()
    {
        base.Start();

        ShopItemSet();
    }

    private void ShopItemSet()
    {
        tableData = new List<ItemData>();
        foreach (var shopItemId in ShopItems.ShopItems)
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
