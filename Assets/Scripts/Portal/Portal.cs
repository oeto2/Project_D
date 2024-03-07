using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Portal : MonoBehaviour
{
    private float _time;
    public GameObject portalUI;
    public Slider loadingBar;
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("TriggerOn");
        portalUI.SetActive(true);
        if (other.gameObject.CompareTag("Player"))
        {
            portalUI.SetActive(true);
        }
    }
    private void OnTriggerStay(Collider other)
    {
        //F키를 눌렀는지 확인
        if (Input.GetKey(KeyCode.E))
        {
            _time += Time.deltaTime;
            loadingBar.value = _time;
            Debug.Log(_time);
            if (_time >= 3)
            {
                //플레이어 씬이동

            }
        }
        else
        {
            _time = 0;
            loadingBar.value = _time;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        Debug.Log("ExitTrigger");
        _time = 0;
        loadingBar.value = _time;
        portalUI.SetActive(false);
    }
}
