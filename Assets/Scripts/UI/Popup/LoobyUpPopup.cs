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
    [SerializeField] private Button _classButton;

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
        _optionButton.onClick.AddListener(() => _uiManager.ShowPopup<OptionPopup>());
        _shopButton.onClick.AddListener(() => ShopButtonClick());
        _enterDungeonButton.onClick.AddListener(() => _uiManager.ShowPopup<EnterDungeonPopup>());
        _stateButton.onClick.AddListener(() => _uiManager.ShowPopup<StatePopup>());
        _inventroyButton.onClick.AddListener(() => InventoryButtonClick());
        _classButton.onClick.AddListener(() => _uiManager.ShowPopup<ClassPopup>());
    }

    private void ShopButtonClick()
    {
        _uiManager.ShowPopup<ShopPopup>();
        _uiManager.ShowPopup<InventoryPopup>();
        _uiManager.ShowPopup<DragPopup>();
    }

    private void InventoryButtonClick()
    {
        _uiManager.ShowPopup<EquipmentPopup>();
        _uiManager.ShowPopup<InventoryPopup>();
        _uiManager.ShowPopup<DragPopup>();
    }

    private void ShopButtonClick()
    {
        _uiManager.ShowPopup<ShopPopup>();
        _uiManager.ShowPopup<InventoryPopup>();
        _uiManager.ShowPopup<DragPopup>();
    }
}
