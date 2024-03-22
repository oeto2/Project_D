using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InventoryPopup : UIBase
{
    private const string _lobbySceneName = "LobbyScene";
    private GameObject lobbyUpPopup_Object;
    private string _currentSceneName;

    private void Awake()
    {
        lobbyUpPopup_Object = UIManager.Instance.GetPopup(nameof(LobbyUpPopup));
        btnClose.onClick.AddListener(() => CloseUI());
        _currentSceneName = SceneManager.GetActiveScene().name;
    }

    private void OnEnable()
    {
        lobbyUpPopup_Object.SetActive(false);
    }

    protected override void CloseUI()
    {
        //로딩 씬에서만 동작
        if (_currentSceneName == _lobbySceneName)
            lobbyUpPopup_Object.SetActive(true);

        gameObject.SetActive(false);
    }
}
