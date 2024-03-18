using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadingSceneUI : MonoBehaviour
{
    private void Awake()
    {
        //�θ� ������Ʈ ����
        UIManager.Instance.parentsUI = transform;
        UIManager.Instance.ShowPopup<LoadingMainPopup>(transform);
    }
}
