using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopPopup : UIBase
{
    [Header("Shop Item")]
    public List<ItemData> ShopItems = new List<ItemData>();

    [Header("Shop UI")]
    public TMP_Text ItemName;
    public TMP_Text ItemPrice;
    public TMP_InputField ItemStack;
    public Image ItemSprite;

    public ItemData ItemData;

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

    private void ShopItemSet(int id)
    {
        // isShop이 True인 데이터들만 가져오기
        for(int i = 0; i < ShopItems.Count; i++)
        {
            
        }
    }

    public void UpdateSlot()
    {
        // 슬롯 훑어서 빈 슬롯에 ShopItem 넣기
    }

    // Button event 만들어서 InputField 갯수만큼 인벤토리에 추가
}
