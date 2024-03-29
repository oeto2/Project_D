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
        //Debug.Log("���â ��Ȱ��ȭ");
        UIManager.Instance.BattleUICount--;

        //UI�� ��� ���� �Ǿ����� �ٽ� Ŀ�� ��
        if (_currentSceneName != _lobbySceneName && UIManager.Instance.BattleUICount <= 0)
            Cursor.lockState = CursorLockMode.Locked;
    }

    private void Awake()
    {
        btnClose.onClick.AddListener(() => CloseUI());

        _currentSceneName = SceneManager.GetActiveScene().name;

        //�κ�������� ��ư ����
        if (_currentSceneName == _lobbySceneName)
        {
            btnClose.gameObject.SetActive(false);
        }

        UIManager.Instance.ShowPopup<DragPopup>();
    }
}
