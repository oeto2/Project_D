using Constants;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameEndPopup : UIBase
{
    [SerializeField] private Button _enterButton;

    private void Awake()
    {
        Cursor.lockState = CursorLockMode.None;
        UIManager.Instance.ShowPopup<InventoryPopup>();
        UIManager.Instance.ShowPopup<EquipmentPopup>();
        //�� �̵�
        _enterButton.onClick.AddListener(() => GameManager.Instance.ChangeScene(SceneType.LobbyScene));
    }
}
