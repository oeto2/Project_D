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
    
    [SerializeField] private DungeonType _level = DungeonType.Farming;
    [SerializeField] private Image _dungeonPopup_BG;
    [SerializeField] private TextMeshProUGUI _titleText;
    [SerializeField] private Sprite[] dungeonImages = new Sprite[2];

    private const string _esayLevelName = "쉬움";
    private const string _normalLevelName = "보통";
    private const string _hardLevelName = "어려움";
    private GameObject lobbyUpPopup_Object;

    private void Awake()
    {
        gameObject.GetComponentInParent<LobbySceneUI>().curLobbyType = LobbyType.EnterDungeon;
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
        if (_level < DungeonType.Count - 1)
            _level++;

        else _level = DungeonType.Farming;

        RefreshDungeonUI();
    }

    private void BeforeButtonClick()
    {
        if (_level >= DungeonType.Farming + 1)
            _level--;

        else _level = DungeonType.Count - 1;

        RefreshDungeonUI();
    }

    //던전 입장 UI 새로고침
    private void RefreshDungeonUI()
    {
        switch(_level)
        {
            case DungeonType.Farming:
                _titleText.text = "잊혀진 이들의 쉼터";
                _dungeonPopup_BG.sprite = dungeonImages[0];
                SelectEnterDungeon(SceneType.DungeonScene);
                break;

            case DungeonType.OrkWarrior:
                _titleText.text = "오크 전사";
                _dungeonPopup_BG.sprite = dungeonImages[1];
                SelectEnterDungeon(SceneType.OrkWarriorScene);
                break;

            case DungeonType.OrkAssasin:
                _titleText.text = "오크 어쌔신";
                _dungeonPopup_BG.sprite = dungeonImages[2];
                SelectEnterDungeon(SceneType.OrkOrkAssasinScene);
                break;

            case DungeonType.Necromancer:
                _titleText.text = "네크로맨서";
                _dungeonPopup_BG.sprite = dungeonImages[3];
                SelectEnterDungeon(SceneType.NecromancerScene);
                break;
        }
    }

    //입장할 던전 선택
    private void SelectEnterDungeon(SceneType sceneType_)
    {
        _enterButton.onClick.RemoveAllListeners();
        _enterButton.onClick.AddListener(() => GameManager.Instance.ChangeScene(sceneType_));
    }
}
