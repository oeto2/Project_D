using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadingSceneUI : MonoBehaviour
{
    private void Awake()
    {
        //�θ� ������Ʈ ����
        UIManager.Instance.parentsUI = transform;
        LoadingMainPopup loadingMainPopup = UIManager.Instance.ShowPopup<LoadingMainPopup>(transform);

        LoadingSceneController loadingSceneController = ResourceManager.Instance.Instantiate("Manager/LodingSceneManager").GetComponent<LoadingSceneController>();
        //loadingSceneController.LoadingBar = loadingMainPopup.
    }
}
