using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopPopup : UIBase
{
    [Header("Shop Item")]
    public List<ItemData> Items = new List<ItemData>();
    public List<ItemData> ShopItems = new List<ItemData>();

    [Header("Shop UI")]
    public TMP_Text ItemName;
    public TMP_Text ItemPrice;
    public Image ItemSprite;

    private GameObject lobbyUpPopup_Object;

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

    private void ShopItemSet()
    {
        Items.Add(Database.Item.Get(20000001));
        Items.Add(Database.Item.Get(20000002));
    }

    public void UpdateSlot()
    {
        // ½½·Ô ÈÈ¾î¼­ ºó ½½·Ô¿¡ ShopItem ³Ö±â
    }

    // Button event ¸¸µé¾î¼­ buy¹öÆ° ¶ç¿ì±â

    // 
}
