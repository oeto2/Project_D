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
    public Player player;

    public SceneType sceneType = SceneType.TutorialScene;

    private const string _tutorialSceneName = "TutorialScene";
    private const string _dungeonSceneName = "DungeonScene";
    private const string _loadingSceneName = "LoadingScene";

    public event Action SceneLoadEvent;

    //리워드 보상을 가져갈때 호출되는 이벤트들
    public event Action<List<int>> GetRewardItemEvent;
    //변경된 리워드 보상을 적용하는 이벤트
    public event Func<List<int>> SetRewardItemEvent;
    //리워드 창을 닫을 때 호출되는 이벤트
    public event Action CloseRewardPopupEvent;

    private void Awake()
    {
        SceneManager.sceneLoaded += PlayerInit;
    }
    private void Start()
    {
        //아이템 데이터 세팅
        Database.DropPer.ItemEnum();
    }

    private void PlayerInit(Scene scene, LoadSceneMode mode)
    {
        if (playerObject == null && (SceneManager.GetActiveScene().name == _dungeonSceneName|| SceneManager.GetActiveScene().name == _tutorialSceneName))
        {
            playerObject = ResourceManager.Instance.Instantiate("Player/Player");
            player = playerObject.GetComponent<Player>();
        }

        else if (sceneType == SceneType.LobbyScene)
        {
            playerObject = ResourceManager.Instance.Instantiate("Player/Player");
            player = playerObject.GetComponent<Player>();
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

    //보상 목록 이벤트 호출
    public void CallGetRewardItemEvent(List<int> itemsId)
    {
        GetRewardItemEvent?.Invoke(itemsId);
    }

    // 변경된 보상 목록 적용 이벤트 호출 
    public List<int> CallSetRewardItemEvent()
    {
        return SetRewardItemEvent?.Invoke();
    }

    //보상목록 이벤트 전부 해제
    public void ClearGetRewardItemEvent()
    {
        GetRewardItemEvent = null;
    }

    public void UsePotion(ItemData potion)
    {
        player.Stats.UsePotion(potion);
    }

    public void CallCloseRewardPopupEvent()
    {
        CloseRewardPopupEvent?.Invoke();
        CloseRewardPopupEvent = null;
    }
}
