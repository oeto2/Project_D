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
        //¾À ÀÌµ¿
        _enterButton.onClick.AddListener(() => EnterButtonClick());
    }

    private void EnterButtonClick()
    {
        if (!InformationManager.Instance.saveLoadData.isTutorialClear)
        {
            GameManager.Instance.ChangeScene(SceneType.TutorialScene);
            return;
        }

        GameManager.Instance.ChangeScene(SceneType.LobbyScene);
    }
}
