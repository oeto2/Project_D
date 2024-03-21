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
        }

        AsyncOperation loadScene = SceneManager.LoadSceneAsync(scene);
        loadScene.allowSceneActivation = false;

        float elapsedTime = 0f;
        float targetTime = 7f;
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
