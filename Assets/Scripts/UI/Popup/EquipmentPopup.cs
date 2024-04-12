using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EquipmentPopup : UIBase
{
    private const string _lobbySceneName = "LobbyScene";
    private string _currentSceneName;

    private void OnEnable()
    {
        UIManager.Instance.BattleUICount++;
    }

    private void OnDisable()
    {
        //Debug.Log("장비창 비활성화");
        UIManager.Instance.BattleUICount--;

        //UI가 모두 종료 되었으면 다시 커서 락
        if (_currentSceneName != _lobbySceneName && UIManager.Instance.BattleUICount <= 0)
            Cursor.lockState = CursorLockMode.Locked;
    }

    private void Awake()
    {
        btnClose.onClick.AddListener(() => CloseUI());

        _currentSceneName = SceneManager.GetActiveScene().name;

        //로비씬에서는 버튼 숨김
        if (_currentSceneName == _lobbySceneName)
        {
            btnClose.gameObject.SetActive(false);
        }

        UIManager.Instance.ShowPopup<DragPopup>();
    }
}
