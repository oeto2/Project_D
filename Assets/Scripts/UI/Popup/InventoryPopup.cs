using Constants;
using UnityEngine;
using UnityEngine.SceneManagement;

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
        UIManager.Instance.ShowPopup<DragPopup>();
    }

    private void OnEnable()
    {
        if (SceneManager.GetActiveScene().name == _lobbySceneName)
            UIManager.Instance.GetPopupObject(nameof(LobbyUpPopup)).gameObject?.SetActive(false);
    }

    private void OnDisable()
    {
        ItemDescription.instance.gameObject.SetActive(false);
    }

    protected override void CloseUI()
    {
        if (_lobbySceneUI?.curLobbyType == LobbyType.Storage)
        {
            gameObject.SetActive(false);
        }

        //로비 씬에서만 동작
        if (_currentSceneName == _lobbySceneName)
        {
            UIManager.Instance.GetPopup(nameof(ShopPopup))?.gameObject.SetActive(false);
            UIManager.Instance.GetPopup(nameof(EquipmentPopup)).gameObject.SetActive(false);
            UIManager.Instance.GetPopup(nameof(DragPopup)).gameObject.SetActive(false);
            UIManager.Instance.GetPopup(nameof(StoragePopup)).gameObject.SetActive(false);
            UIManager.Instance.GetPopup(nameof(LobbyUpPopup)).gameObject.SetActive(true);
        }

        gameObject.SetActive(false);
    }
}
