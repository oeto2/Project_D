using Constants;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Portal : MonoBehaviour, IInteractable
{
    private UIManager _uiManager;
    private interationPopup _interationPopup;
    private float _time;
    private Slider _loadingBar;
    private const string _tutorialSceneName = "TutorialScene";

    private void Awake()
    {
        _uiManager = UIManager.Instance;
    }

    private void Start()
    {
        _interationPopup = _uiManager.GetPopup(nameof(interationPopup)).GetComponent<interationPopup>();

        _loadingBar = _interationPopup.LoadingBar;
    }

    string IInteractable.GetInteractPrompt()
    {
        _time = 0;
        return string.Format("Use Portal");
    }

    void IInteractable.OnInteract()
    {
        //Debug.Log("포탈 상호작용 시작");
        _loadingBar.gameObject.SetActive(true);
        _time += Time.deltaTime;
        _loadingBar.value = _time / 3;

        //상호작용 완료
        if (_time >= 3)
        {
            if (SceneManager.GetActiveScene().name == _tutorialSceneName)
                InformationManager.Instance.saveLoadData.isTutorialClear = true;

            UIManager.Instance.ShowPopup<GameEndPopup>();
        }
    }
    void IInteractable.CancelInteract()
    {
        _loadingBar.gameObject.SetActive(false);
        _time = 0;
        _loadingBar.value = _time / 3;
    }
}
