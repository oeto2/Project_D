using Constants;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LoadingMainPopup : UIBase
{
    public Slider loadingBar;
    [SerializeField] private Image loadingImage;
    [SerializeField] private TMP_Text tilteText;
    [SerializeField] private TMP_Text loadingText;
    [SerializeField] private TMP_Text tooltipText;

    //현재 로딩중인 씬
    private SceneType curLoadSceneType;

    private void Awake()
    {
        curLoadSceneType = GameManager.Instance.sceneType;
    }

    private void Start()
    {
        InitLoadingSceneUI();
    }

    private void InitLoadingSceneUI()
    {
        switch(curLoadSceneType)
        {
            case SceneType.LobbyScene:
                tilteText.text = "베이스 캠프";
                loadingText.text = "던전에서 나가는 중..";
                tooltipText.text = "획득한 아이템은 상점에서 판매가 가능합니다.";
                break;

            case SceneType.DungeonScene:
                tilteText.text = "잊혀진 이들의 쉼터";
                loadingText.text = "던전 입장 중..";
                tooltipText.text = "스켈레톤은 이동속도가 느립니다.";
                break;

            case SceneType.TutorialScene:
                tilteText.text = "튜토리얼";
                loadingText.text = "튜토리얼 불러오는중..";
                tooltipText.text = "죽으면 아이템을 모두 잃습니다.";
                break;
        }
    }
}
