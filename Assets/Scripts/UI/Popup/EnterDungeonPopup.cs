using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnterDungeonPopup : UIBase
{
    [SerializeField] private Button _enterBtn;

    private void Awake()
    {
        base.CloseUI();
        SetButtons();
    }

    //��ư ����
    private void SetButtons()
    {
        //�� ����
        //_enterBtn.onClick.AddListener(() => )
    }
}
