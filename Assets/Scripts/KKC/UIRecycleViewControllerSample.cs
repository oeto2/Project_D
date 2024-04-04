using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace UI
{
    public class UIRecycleViewControllerSample : UIRecycleViewController<ItemData>
    {
        [field: SerializeField] public ShopListSO ShopItems;
        //[SerializeField] private Transform _shopItemSlots;

        // 리스트 항목의 데이터를 읽어 들이는 메서드
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
                //var shopSlot = ResourceManager.Instance.Instantiate("UI/ShopSlot", _shopItemSlots);
                //shopSlot.GetComponent<ShopSlot>().UpdateUI(shopItem);
                //Debug.Log(Database.Item.Get(shopItemId).itemName);
            }
            
            foreach (var item in tableData)
            {
                Debug.Log(item.id);
            }

            InitializeTableView();
        }
    }
}
