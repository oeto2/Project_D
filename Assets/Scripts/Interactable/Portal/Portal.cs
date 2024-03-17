using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Portal : MonoBehaviour, IInteractable
{
    private float _time;
    private Slider _loadingBar;

    private void Start()
    {
        _loadingBar = UIManager.Instance.loadingBar;
    }

    string IInteractable.GetInteractPrompt()
    {
        _time = 0;
        return string.Format("Use Portal");
    }

    void IInteractable.OnInteract()
    {
        _loadingBar.gameObject.SetActive(true);
        _time += Time.deltaTime;
        _loadingBar.value = _time / 3;
        if (_time >= 3)
        {
            //플레이어 씬이동
        }
    }
    void IInteractable.CancelInteract()
    {
        _loadingBar.gameObject.SetActive(false);
        _time = 0;
        _loadingBar.value = _time / 3;
    }
}
