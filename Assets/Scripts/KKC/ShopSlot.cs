using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopSlot : UIRecycleViewCell<ItemData>
{
    [Header("Item Information")]
    public TMP_Text ItemName;
    public TMP_Text ItemPrice;
    public TMP_Text ItemDescription;
    public TMP_InputField ItemStack;
    public Button BuyItemButton;
    public Image ItemSprite;

    public ItemData itemData;

    public override void UpdateContent(ItemData itemData_)
    {
        itemData = itemData_;
        ItemName.text = itemData_.itemName;
        ItemPrice.text = itemData_.itemPrice.ToString();
        ItemDescription.text = itemData_.itemDescription;
        ItemSprite.sprite = ResourceManager.Instance.Load<Sprite>(itemData.itemSprite);
        BuyItemButton.onClick.AddListener(() => OnBuyButton());
    }

    // ������ ���� ��ư
    public void OnBuyButton()
    {
        int itemCount; // ������ ����
        if (int.TryParse(ItemStack.text, out itemCount))
        {
            if (itemCount > 0 && InformationManager.Instance.saveLoadData.gold >= itemData.itemPrice * itemCount)
            {
                Inventory inventory = UIManager.Instance.GetPopupObject(nameof(InventoryPopup)).GetComponent<Inventory>();
                inventory.AcquireItem(itemData, itemCount);
                InformationManager.Instance.InvenGoldChange(-itemData.itemPrice * itemCount);

            }
            else
            {
                UIManager.Instance.ShowPopup<WarningPopup>();
            }
        }
        ItemStack.text = null;
    }
}