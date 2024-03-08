using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Portal : MonoBehaviour, IInteractable
{
    private float _time;
    public Slider loadingBar;
    //private void Start()
    //{
    //    loadingBar = GameObject.Find("LoadingBar").GetComponent<Slider>();
    //}
    //private void OnTriggerEnter(Collider other)
    //{
    //    Debug.Log("TriggerOn");
    //    portalUI.SetActive(true);
    //    if (other.gameObject.CompareTag("Player"))
    //    {
    //        portalUI.SetActive(true);
    //    }
    //}
    //private void OnTriggerStay(Collider other)
    //{
    //    if (Input.GetKey(KeyCode.E))
    //    {
    //        _time += Time.deltaTime;
    //        loadingBar.value = _time / 3;
    //        Debug.Log(_time);
    //        if (_time >= 3)
    //        {
    //            //플레이어 씬이동

    //        }
    //    }
    //    else
    //    {
    //        _time = 0;
    //        loadingBar.value = _time / 3;

    //    }
    //}
    //private void OnTriggerExit(Collider other)
    //{
    //    Debug.Log("ExitTrigger");
    //    _time = 0;
    //    loadingBar.value = _time / 3;
    //    portalUI.SetActive(false);
    //}

    string IInteractable.GetInteractPrompt()
    {
        return string.Format("Use Portal");
    }

    void IInteractable.OnInteract()
    {
        loadingBar.gameObject.SetActive(true);
        _time += Time.deltaTime;
        loadingBar.value = _time / 3;
        if (_time >= 3)
        {
            //플레이어 씬이동

        }
    }
    void IInteractable.CancelInteract()
    {
        loadingBar.gameObject.SetActive(false);
        _time = 0;
        loadingBar.value = _time / 3;
    }
}
