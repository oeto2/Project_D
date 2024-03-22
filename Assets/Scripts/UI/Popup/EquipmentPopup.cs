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
        UIManager.Instance.BattleUICount--;
    }

    private void Awake()
    {
        _currentSceneName = SceneManager.GetActiveScene().name;
        base.CloseUI();
        //�κ�������� ��ư ����
        if (_currentSceneName == _lobbySceneName)
        {
            btnClose.gameObject.SetActive(false);
        }
    }
}
