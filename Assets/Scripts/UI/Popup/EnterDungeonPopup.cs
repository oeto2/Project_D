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

    //버튼 세팅
    private void SetButtons()
    {
        //씬 연결
        //_enterBtn.onClick.AddListener(() => )
    }
}
