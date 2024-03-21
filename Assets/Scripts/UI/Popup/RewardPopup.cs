using Constants;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RewardPopup : UIBase
{
    [SerializeField] private Button _enterButton;

    private void Awake()
    {
        Cursor.lockState = CursorLockMode.None;
        //씬 이동
        _enterButton.onClick.AddListener(() => GameManager.Instance.ChangeScene(SceneType.LobbyScene));
        _enterButton.onClick.AddListener(() => Debug.Log("버튼 클릭"));
    }
}
