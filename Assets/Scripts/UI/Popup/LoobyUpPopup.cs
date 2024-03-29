using Constants;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LobbyUpPopup : UIBase
{
    [SerializeField] private Button _optionButton;
    [SerializeField] private Button _shopButton;
    [SerializeField] private Button _enterDungeonButton;
    [SerializeField] private Button _stateButton;
    [SerializeField] private Button _inventroyButton;
    [SerializeField] private Button _storageButton;

    private UIManager _uiManager;

    private void Awake()
    {
        _uiManager = UIManager.Instance;
    }

    private void Start()
    {
        SettingButtons();
    }

    private void SettingButtons()
    {
        _optionButton.onClick.AddListener(() => OptionButtonClick());
        _shopButton.onClick.AddListener(() => ShopButtonClick());
        _enterDungeonButton.onClick.AddListener(() => EnterDungeonButtonClick());
        _stateButton.onClick.AddListener(() => StateButtonClick());
        _inventroyButton.onClick.AddListener(() => InventoryButtonClick());
        _storageButton.onClick.AddListener(() => StorageButtonClick());
    }

    private void ShopButtonClick()
    {
        GetComponentInParent<LobbySceneUI>().curLobbyType = LobbyType.Shop;
        _uiManager.ShowPopup<ShopPopup>();
        _uiManager.ShowPopup<InventoryPopup>();
        _uiManager.ShowPopup<DragPopup>();
    }

    private void InventoryButtonClick()
    {
        GetComponentInParent<LobbySceneUI>().curLobbyType = LobbyType.Inventory;
        _uiManager.ShowPopup<EquipmentPopup>();
        _uiManager.ShowPopup<InventoryPopup>();
        _uiManager.ShowPopup<DragPopup>();
    }

    private void StorageButtonClick()
    {
        GetComponentInParent<LobbySceneUI>().curLobbyType = LobbyType.Storage;
        _uiManager.ShowPopup<StoragePopup>();
        _uiManager.ShowPopup<InventoryPopup>();
        _uiManager.ShowPopup<DragPopup>();
    }

    private void StateButtonClick()
    {
        GetComponentInParent<LobbySceneUI>().curLobbyType = LobbyType.Status;
        _uiManager.ShowPopup<StatePopup>();
    }

    private void EnterDungeonButtonClick()
    {
        GetComponentInParent<LobbySceneUI>().curLobbyType = LobbyType.EnterDungeon;
        _uiManager.ShowPopup<EnterDungeonPopup>();
    }

    private void OptionButtonClick()
    {
        GetComponentInParent<LobbySceneUI>().curLobbyType = LobbyType.Option;
        _uiManager.ShowPopup<OptionPopup>();
    }
}
