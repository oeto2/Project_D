using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadingSceneUI : MonoBehaviour
{
    private void Awake()
    {
        //부모 오브젝트 지정
        UIManager.Instance.parentsUI = transform;
        UIManager.Instance.ShowPopup<LoadingMainPopup>(transform);
    }
}
