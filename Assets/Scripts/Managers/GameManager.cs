using Constants;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : SingletonBase<GameManager>
{
    public GameObject playerObject;
    public Player player;

    public SceneType sceneType = SceneType.LobbyScene;

    private const string _tutorialSceneName = "TutorialScene";
    private const string _dungeonSceneName = "DungeonScene";
    private const string _loadingSceneName = "LoadingScene";
    private const string _OrkWarriorSceneName = "OrkWarriorScene";
    private const string _OrkAssasinSceneName = "OrkAssasinScene";
    private const string _necromancerSceneName = "NecromancerScene";

    public event Action SceneLoadEvent;

    //리워드 보상을 가져갈때 호출되는 이벤트들
    public event Action<List<int>> GetRewardItemEvent;
    //변경된 리워드 보상을 적용하는 이벤트
    public event Func<List<int>> SetRewardItemEvent;
    //리워드 창 아이템 갯수 업데이트 이벤트
    public event Action UpdateRewardCountEvent;

    private void Awake()
    {
        SceneManager.sceneLoaded += PlayerInit;

        //튜토리얼을 진행하지 않았다면 씬전환
        if (!InformationManager.Instance.saveLoadData.isTutorialClear)
            ChangeScene(SceneType.TutorialScene);
    }
    private void Start()
    {
        //아이템 데이터 세팅
        Database.DropPer.ItemEnum();
    }

    private void PlayerInit(Scene scene, LoadSceneMode mode)
    {
        string sceneName = SceneManager.GetActiveScene().name;

        if (playerObject == null && (sceneName == _dungeonSceneName || sceneName == _tutorialSceneName 
            || sceneName == _OrkWarriorSceneName|| sceneName == _necromancerSceneName || sceneName == _OrkAssasinSceneName))
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

    //리워드 아이템 획득 이벤트 호출
    public void CallGetRewardItemEvent(List<int> itemsId)
    {
        GetRewardItemEvent?.Invoke(itemsId);
    }

    //리워드 아이템을 넘겨주는 이벤트 호출
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

    public void CallUpdateRewardCountEvent()
    {
        UpdateRewardCountEvent?.Invoke();
    }

    //아이템 갯수 카운트 업데이트 이벤트 전부취소
    public void CancelUpdateRewardCountEvent()
    {
        UpdateRewardCountEvent = null;
    }
}
