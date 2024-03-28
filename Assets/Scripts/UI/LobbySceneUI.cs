using Constants;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbySceneUI : MonoBehaviour
{
    public LobbyType curLobbyType = LobbyType.Main;

    private void Awake()
    {
        //�θ� UI�� ����
        UIManager.Instance.parentsUI = transform;
        UIManager.Instance.ShowPopup("LobbyUpPopup", transform);
    }
}
