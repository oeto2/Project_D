using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbySceneUI : MonoBehaviour
{
    private void Awake()
    {
        //부모 UI로 설정
        UIManager.Instance.parentsUI = transform;
        UIManager.Instance.ShowPopup("LobbyUpPopup", transform);
    }
}
