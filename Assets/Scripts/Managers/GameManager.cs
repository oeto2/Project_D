using Constants;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : SingletonBase<GameManager>
{
    public GameObject playerObject;

    public SceneType sceneType = SceneType.LobbyScene;

    private const string _dungeonSceneName = "DungeonScene";
    private const string _loadingSceneName = "LoadingScene";

    private void Awake()
    {
        SceneManager.sceneLoaded += PlayerInit;
    }

    private void PlayerInit(Scene scene, LoadSceneMode mode)
    {
        if (playerObject == null && SceneManager.GetActiveScene().name == _dungeonSceneName)
        {
            playerObject = ResourceManager.Instance.Instantiate("Player/Player");
            //playerObject = Resources.Load<GameObject>("Prefabs/Player/Player");
            //  Instantiate(playerObject);
        }
    }

    public void ChangeScene(SceneType scene)
    {
        sceneType = scene;
        SceneManager.LoadScene(_loadingSceneName);
    }
}
