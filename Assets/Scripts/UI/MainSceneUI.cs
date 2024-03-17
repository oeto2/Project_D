using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainSceneUI : MonoBehaviour
{
    private void Awake()
    {
        //�θ� UI�� ����
        UIManager.Instance.parentsUI = transform;
        UIManager.Instance.ShowPopup("MainUpPopup", transform);
    }
}
