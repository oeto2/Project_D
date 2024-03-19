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

        float timer = 0f;
        while (!loadScene.isDone)
        {
            yield return null;

            if (loadScene.progress < 0.5f)
            {
                LoadingBar.value = loadScene.progress;
            }
            else
            {
                timer += Time.unscaledDeltaTime;
                LoadingBar.value = Mathf.Lerp(0.5f, 1f, timer);
                if (LoadingBar.value >= 1f)
                {
                    loadScene.allowSceneActivation = true;
                    yield break;
                }
            }
        }
        yield return null;
    }
}
