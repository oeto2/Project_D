using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatePopup : UIBase
{
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
}
