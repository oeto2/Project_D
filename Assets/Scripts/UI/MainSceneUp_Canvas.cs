using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainSceneUP_Canvas : UIBase
{
    [SerializeField] private Button _optionButton;
    [SerializeField] private Button _shopButton;
    [SerializeField] private Button _dungeonButton;
    [SerializeField] private Button _stateButton;
    [SerializeField] private Button _inventoryButton;
    [SerializeField] private Button _classButton;

    private void Awake()
    {
        SetButton();
    }

    private void SetButton()
    {
    }
}
