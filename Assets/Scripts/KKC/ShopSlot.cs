using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopSlot : MonoBehaviour
{
    [Header("Item Information")]
    public TMP_Text ItemName;
    public TMP_Text ItemPrice;
    public TMP_Text ItemDescription;
    public TMP_InputField ItemStack;
    public Button BuyItemButton;
    public Image ItemSprite;

    public ItemData ItemData;

    public void UpdateUI(ItemData itemData_)
    {
        ItemData = itemData_;
        ItemName.text = ItemData.itemName;
        ItemPrice.text = ItemData.itemPrice.ToString();
        ItemDescription.text = ItemData.itemDescription;
        ItemSprite.sprite = ResourceManager.Instance.Load<Sprite>(ItemData.itemSprite);
        BuyItemButton.onClick.AddListener(() => OnBuyButton());
    }

    // ������ ���� ��ư
    public void OnBuyButton()
    {
        int itemCount; // ������ ����
        if (int.TryParse(ItemStack.text, out itemCount))
        {
            if (ItemStack.text == null)
            {
                itemCount = 1;
                //Inventory.Instance.AcquireItem(ItemData, itemCount);
            }
            Debug.Log(ItemData.itemName + " " + itemCount);
        }
        ItemStack.text = null;
        //else if() // ��� ������ ��
    }
}
