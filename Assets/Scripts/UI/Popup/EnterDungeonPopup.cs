using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EnterDungeonPopup : UIBase
{
    [SerializeField] private Button _enterBtn;
    private GameObject lobbyUpPopup_Object;
    private const string _loadingSceneName = "LoadingScene";

    private void Awake()
    {
        lobbyUpPopup_Object = UIManager.Instance.GetPopup(nameof(LobbyUpPopup));
        btnClose.onClick.AddListener(() => CloseUI());
        _enterBtn.onClick.AddListener(() => SceneManager.LoadScene(_loadingSceneName));
    }

    private void OnEnable()
    {
        lobbyUpPopup_Object.SetActive(false);
    }

    protected override void CloseUI()
    {
        lobbyUpPopup_Object.SetActive(true);
        gameObject.SetActive(false);
    }
}
