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
        //Ŀ�� �� Ǯ��
        Cursor.lockState = CursorLockMode.None;
        UIManager.Instance.BattleUICount++;
    }

    private void OnDisable()
    {
        UIManager.Instance.BattleUICount--;

        //���� �̺�Ʈ ��� ����
        GameManager.Instance.ClearGetRewardItemEvent();

        //UI�� ��� ���� �Ǿ����� �ٽ� Ŀ�� ��
        if (_currentSceneName != _lobbySceneName && UIManager.Instance.BattleUICount <= 0)
            Cursor.lockState = CursorLockMode.Locked;
    }
}
