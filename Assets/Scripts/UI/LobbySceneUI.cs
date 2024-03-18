using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbySceneUI : MonoBehaviour
{
    private void Awake()
    {
        //�θ� UI�� ����
        UIManager.Instance.parentsUI = transform;
        UIManager.Instance.ShowPopup("LobbyUpPopup", transform);
    }
}
