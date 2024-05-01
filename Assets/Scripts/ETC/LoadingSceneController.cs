using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Constants;

public class LoadingSceneController : MonoBehaviour
{
    public const string lobbyScene = "LobbyScene";
    public const string dungeonScene = "DungeonScene";
    private const string _tutorialSceneName = "TutorialScene";
    private const string _orkWarriorScene = "OrkWarriorScene";
    private const string _orkAssasinScene = "OrkAssasinScene";
    private const string _necromancerScene = "NecromancerScene";

    public Slider LoadingBar;

    public void Start()
    {
        StartCoroutine(LoadSceneProcess());
    }

    private IEnumerator LoadSceneProcess()
    {
        string scene = lobbyScene;

        switch (GameManager.Instance.sceneType)
        {
            case SceneType.LobbyScene:
                scene = lobbyScene;
                break;
            case SceneType.DungeonScene:
                scene = dungeonScene;
                break;
            case SceneType.TutorialScene:
                scene = _tutorialSceneName;
                break;
            case SceneType.OrkWarriorScene:
                scene = _orkWarriorScene;
                break;
            case SceneType.OrkOrkAssasinScene:
                scene = _orkAssasinScene;
                break;
            case SceneType.NecromancerScene:
                scene = _necromancerScene;
                break;
        }

        AsyncOperation loadScene = SceneManager.LoadSceneAsync(scene);
        loadScene.allowSceneActivation = false;

        float elapsedTime = 0f;
        float targetTime = 2f;
        while (!loadScene.isDone && !loadScene.allowSceneActivation)
        {
            elapsedTime += Time.unscaledDeltaTime;
            LoadingBar.value = Mathf.Clamp01(elapsedTime / targetTime);

            if (LoadingBar.value >= 1f)
            {
                loadScene.allowSceneActivation = true;
            }
            
            yield return null;
        }
        yield return null;
    }
}
