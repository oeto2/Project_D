using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RewardPopup : UIBase
{
    private const string _lobbySceneName = "LobbyScene";
    private string _currentSceneName;

    private void Awake()
    {
        btnClose?.onClick.AddListener(() => CloseUI());
        _currentSceneName = SceneManager.GetActiveScene().name;
        UIManager.Instance.ShowPopup<DragPopup>();
    }

    private void OnEnable()
    {
        //커서 락 풀기
        Cursor.lockState = CursorLockMode.None;
        UIManager.Instance.BattleUICount++;
    }

    private void OnDisable()
    {
        UIManager.Instance.BattleUICount--;

        //보상 이벤트 목록 정리
        GameManager.Instance.ClearGetRewardItemEvent();

        //UI가 모두 종료 되었으면 다시 커서 락
        if (_currentSceneName != _lobbySceneName && UIManager.Instance.BattleUICount <= 0)
            Cursor.lockState = CursorLockMode.Locked;
    }
}
