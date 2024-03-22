using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OptionPopup : UIBase
{
    private const string _lobbySceneName = "LobbyScene";
    private string _currentSceneName;

    private GameObject lobbyUpPopup_Object;

    private void Awake()
    {
        _currentSceneName = SceneManager.GetActiveScene().name;
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

        //UI가 모두 종료 되었으면 다시 커서 락
        if (_currentSceneName != _lobbySceneName && UIManager.Instance.BattleUICount <= 0)
            Cursor.lockState = CursorLockMode.Locked;
    }

    protected override void CloseUI()
    {
        if (_currentSceneName == _lobbySceneName)
        {
            lobbyUpPopup_Object.SetActive(true);
        }
        gameObject.SetActive(false);
    }
}
