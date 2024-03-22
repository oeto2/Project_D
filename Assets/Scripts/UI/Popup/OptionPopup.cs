using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionPopup : UIBase
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
        UIManager.Instance.BattleUICount++;
    }

    private void OnDisable()
    {
        UIManager.Instance.BattleUICount--;
    }

    protected override void CloseUI()
    {
        lobbyUpPopup_Object.SetActive(true);
        gameObject.SetActive(false);
    }
}
