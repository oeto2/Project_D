using Constants;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoragePopup : UIBase
{
    private void Awake()
    {
        btnClose?.onClick.AddListener(() => CloseUI());   
    }

    protected override void CloseUI()
    {
        gameObject.SetActive(false);
        UIManager.Instance.ShowPopup<LobbyUpPopup>();
        UIManager.Instance.GetPopup(nameof(InventoryPopup)).SetActive(false);
    }
}
