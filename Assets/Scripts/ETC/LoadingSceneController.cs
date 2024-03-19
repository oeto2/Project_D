using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingSceneController : MonoBehaviour
{
    public const string dungeonScene = "LSM_Scene";
    public Slider LoadingBar;

    public void Start()
    {
        StartCoroutine(LoadSceneProcess());
    }

    private IEnumerator LoadSceneProcess()
    {
        AsyncOperation loadScene = SceneManager.LoadSceneAsync(dungeonScene);
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
