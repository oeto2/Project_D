using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MouseSensitivity : MonoBehaviour
{
    public Slider mouseSensitivity;

    private void OnEnable()
    {
        mouseSensitivity.value = InformationManager.Instance.mouseSensitivity * 100;
    }


    public void OnReset()
    {
        InformationManager.Instance.mouseSensitivity = 0.2f;
        mouseSensitivity.value = 20;
    }

    private void OnDisable()
    {
        if (mouseSensitivity.value == 0)
        {
            mouseSensitivity.value = 1;
        }
        InformationManager.Instance.mouseSensitivity = mouseSensitivity.value * 0.01f;
    }

    //public void OnSave()
    //{
    //    if(mouseSensitivity.value == 0)
    //    {
    //        mouseSensitivity.value = 0.02f;
    //    }
    //    _playerController.lookSensitivity = mouseSensitivity.value;
    //}
}
