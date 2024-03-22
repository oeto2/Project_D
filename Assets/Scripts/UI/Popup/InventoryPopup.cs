using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class InventoryPopup : UIBase
{
    private const string _lobbySceneName = "LobbyScene";
    private GameObject lobbyUpPopup_Object;
    private string _currentSceneName;

    private void Awake()
    {
        _currentSceneName = SceneManager.GetActiveScene().name;
        lobbyUpPopup_Object = UIManager.Instance.GetPopup(nameof(LobbyUpPopup));
        btnClose.onClick.AddListener(() => CloseUI());
    }

    private void OnEnable()
    {
        lobbyUpPopup_Object.SetActive(false);
    }

    protected override void CloseUI()
    {
        //로딩 씬에서만 동작
        if (_currentSceneName == _lobbySceneName)
        {
            lobbyUpPopup_Object.SetActive(true);
            UIManager.Instance.GetPopup(nameof(InventoryPopup)).gameObject.SetActive(false);
            UIManager.Instance.GetPopup(nameof(EquipmentPopup)).gameObject.SetActive(false);
            UIManager.Instance.GetPopup(nameof(DragPopup)).gameObject.SetActive(false);
            return;
        }

        gameObject.SetActive(false);
    }
}
