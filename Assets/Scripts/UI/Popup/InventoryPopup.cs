using Constants;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class InventoryPopup : UIBase
{
    private const string _lobbySceneName = "LobbyScene";
    private string _currentSceneName;
    private LobbySceneUI _lobbySceneUI;

    private void Awake()
    {
        _lobbySceneUI = GetComponentInParent<LobbySceneUI>();
        _currentSceneName = SceneManager.GetActiveScene().name;
        btnClose.onClick.AddListener(() => CloseUI());
    }

    private void OnEnable()
    {
        UIManager.Instance.GetPopup(nameof(LobbyUpPopup)).gameObject.SetActive(false);
        UIManager.Instance.BattleUICount++;
    }

    private void OnDisable()
    {
        UIManager.Instance.BattleUICount--;

        //UI�� ��� ���� �Ǿ����� �ٽ� Ŀ�� ��
        if (_currentSceneName != _lobbySceneName && UIManager.Instance.BattleUICount <= 0)
            Cursor.lockState = CursorLockMode.Locked;
    }

    protected override void CloseUI()
    {
        if(_lobbySceneUI?.curLobbyType == LobbyType.Storage)
        {
            gameObject.SetActive(false);
        }    

        //�κ� �������� ����
        if (_currentSceneName == _lobbySceneName && _lobbySceneUI.curLobbyType != LobbyType.Storage)
        {
            UIManager.Instance.GetPopup(nameof(ShopPopup))?.gameObject.SetActive(false);
            UIManager.Instance.GetPopup(nameof(EquipmentPopup)).gameObject.SetActive(false);
            UIManager.Instance.GetPopup(nameof(DragPopup)).gameObject.SetActive(false);
            UIManager.Instance.GetPopup(nameof(LobbyUpPopup)).gameObject.SetActive(true);
        }

        gameObject.SetActive(false);
    }
}
