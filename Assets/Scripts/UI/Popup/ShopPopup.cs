using Constants;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopPopup : UIBase
{
    [field: SerializeField] public ShopListSO ShopItems;
    [SerializeField] private Transform _shopItemSlots;

    private GameObject lobbyUpPopup_Object;

    private void Start()
    {
        ShopItemSet();
    }

    private void Awake()
    {
        lobbyUpPopup_Object = UIManager.Instance.GetPopup(nameof(LobbyUpPopup));
        btnClose.onClick.AddListener(() => CloseUI());
    }

    private void OnEnable()
    {
        lobbyUpPopup_Object.SetActive(false);
    }

    protected override void CloseUI()
    {
        lobbyUpPopup_Object.SetActive(true);
        gameObject.SetActive(false);
    }

    // ������ SO�� �����ϴ� int���� ���� id�� ������ �߰�
    private void ShopItemSet()
    {
        foreach (var shopItemId in ShopItems.ShopItems)
        {
            var shopItem = Database.Item.Get(shopItemId);
            var shopSlot = ResourceManager.Instance.Instantiate("UI/ShopSlot", _shopItemSlots);
            shopSlot.GetComponent<ShopSlot>().UpdateUI(shopItem);
            //Debug.Log(Database.Item.Get(shopItemId).itemName);
        }
    }
}
