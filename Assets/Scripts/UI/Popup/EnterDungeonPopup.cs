using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Constants;
using TMPro;

public class EnterDungeonPopup : UIBase
{
    [SerializeField] private Button _enterButton;
    [SerializeField] private Button _nextButton;
    [SerializeField] private Button _beforeButton;

    [SerializeField] private TextMeshProUGUI _levelText;

    private const string _esayLevelName = "쉬움";
    private const string _normalLevelName = "보통";
    private const string _hardLevelName = "어려움";


    private GameObject lobbyUpPopup_Object;
    [SerializeField] private DungeonLevel _level = DungeonLevel.Esay;

    private void Awake()
    {
        lobbyUpPopup_Object = UIManager.Instance.GetPopup(nameof(LobbyUpPopup));
        btnClose.onClick.AddListener(() => CloseUI());
        _enterButton.onClick.AddListener(() => GameManager.Instance.ChangeScene(SceneType.DungeonScene));
        _nextButton.onClick.AddListener(() => NextButtonClick());
        _beforeButton.onClick.AddListener(() => BeforeButtonClick());
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

    private void NextButtonClick()
    {
        if (_level < DungeonLevel.Count - 1)
            _level++;

        else _level = DungeonLevel.Esay;

        RefreshLevelText();
    }

    private void BeforeButtonClick()
    {
        if (_level >= DungeonLevel.Esay + 1)
            _level--;

        else _level = DungeonLevel.Count - 1;

        RefreshLevelText();
    }

    private void RefreshLevelText()
    {
        switch(_level)
        {
            case DungeonLevel.Esay:
                _levelText.text = _esayLevelName;
                break;

            case DungeonLevel.Normal:
                _levelText.text = _normalLevelName;
                break;

            case DungeonLevel.Hard:
                _levelText.text = _hardLevelName;
                break;
        }
    }
}
