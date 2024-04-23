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

    //���� �ε����� ��
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
                tilteText.text = "���̽� ķ��";
                loadingText.text = "�������� ������ ��..";
                tooltipText.text = "ȹ���� �������� �������� �ǸŰ� �����մϴ�.";
                break;

            case SceneType.DungeonScene:
                tilteText.text = "������ �̵��� ����";
                loadingText.text = "���� ���� ��..";
                tooltipText.text = "���̷����� �̵��ӵ��� �����ϴ�.";
                break;

            case SceneType.TutorialScene:
                tilteText.text = "Ʃ�丮��";
                loadingText.text = "Ʃ�丮�� �ҷ�������..";
                tooltipText.text = "������ �������� ��� �ҽ��ϴ�.";
                break;

            case SceneType.OrkWarriorScene:
                tilteText.text = "��ũ ����";
                loadingText.text = "���� �ҷ�������..";
                tooltipText.text = "��ũ������ ���⸦ �ֽ��ϼ���.";
                break;

            case SceneType.DeathKnightScene:
                tilteText.text = "���� ����Ʈ";
                loadingText.text = "���� �ҷ�������..";
                tooltipText.text = "��Ƽ� ���� �� �����ϴ�.";
                break;
        }
    }
}
