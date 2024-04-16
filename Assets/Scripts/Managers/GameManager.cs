using Constants;
using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GameManager : SingletonBase<GameManager>
{
    public GameObject playerObject;
    private Player _player;

    public SceneType sceneType = SceneType.LobbyScene;

    private const string _tutorialSceneName = "TutorialScene";
    private const string _dungeonSceneName = "DungeonScene";
    private const string _loadingSceneName = "LoadingScene";

    public event Action SceneLoadEvent;

    //������ ������ �������� ȣ��Ǵ� �̺�Ʈ��
    public event Action<List<int>> GetRewardItemEvent;
    //����� ������ ������ �����ϴ� �̺�Ʈ
    public event Func<List<int>> SetRewardItemEvent;

    private void Awake()
    {
        SceneManager.sceneLoaded += PlayerInit;
    }
    private void Start()
    {
        //������ ������ ����
        Database.DropPer.ItemEnum();
    }

    private void PlayerInit(Scene scene, LoadSceneMode mode)
    {
        if (playerObject == null && (SceneManager.GetActiveScene().name == _dungeonSceneName|| SceneManager.GetActiveScene().name == _tutorialSceneName))
        {
            playerObject = ResourceManager.Instance.Instantiate("Player/Player");
            _player = playerObject.GetComponent<Player>();
        }

        if (sceneType == SceneType.LobbyScene)
        {
            Cursor.lockState = CursorLockMode.None;
        }
    }

    public void ChangeScene(SceneType scene)
    {
        CallSceneChangeEvent();
        sceneType = scene;
        SceneManager.LoadScene(_loadingSceneName);
    }

    private void CallSceneChangeEvent()
    {
        SceneLoadEvent?.Invoke();
    }

    //���� ��� �̺�Ʈ ȣ��
    public void CallGetRewardItemEvent(List<int> itemsId)
    {
        GetRewardItemEvent?.Invoke(itemsId);
    }

    // ����� ���� ��� ���� �̺�Ʈ ȣ�� 
    public List<int> CallSetRewardItemEvent()
    {
        return SetRewardItemEvent?.Invoke();
    }

    //������ �̺�Ʈ ���� ����
    public void ClearGetRewardItemEvent()
    {
        GetRewardItemEvent = null;
    }

    public void UsePotion(ItemData potion)
    {
        _player.Stats.UsePotion(potion);
    }
}
