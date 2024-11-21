using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EquipmentPopup : UIBase
{
    private const string _lobbySceneName = "LobbyScene";
    private string _currentSceneName;

    private void OnDisable()
    {
        ItemDescription.instance.gameObject.SetActive(false);
    }

    private void Awake()
    {
        btnClose.onClick.AddListener(() => CloseUI());

        _currentSceneName = SceneManager.GetActiveScene().name;

        //·Îºñ¾À¿¡¼­´Â ¹öÆ° ¼û±è
        if (_currentSceneName == _lobbySceneName)
        {
            btnClose.gameObject.SetActive(false);
        }

        UIManager.Instance.ShowPopup<DragPopup>();
    }
}
