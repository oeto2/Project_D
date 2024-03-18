using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainSceneUI : MonoBehaviour
{
    private void Awake()
    {
        UIManager.Instance.ShowPopup("MainSceneUp_Canvas", transform);
    }
}
