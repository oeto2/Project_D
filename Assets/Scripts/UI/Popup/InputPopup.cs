using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InputPopup : UIBase
{
    public TMP_InputField sellItemCount;
    public Button okButton;
    public Slot sellSlot;

    private void OnEnable()
    {
        sellItemCount.text = null;
    }

    private void Start()
    {
        okButton.onClick.AddListener(() => OnOkButton());
    }

    public void OnOkButton()
    {
        int _sellItemCount = int.Parse(sellItemCount.text);
        if (sellSlot.itemCount - _sellItemCount >= 0)
        {
            sellSlot.AddItem(sellSlot.item, sellSlot.itemCount - _sellItemCount);
            this.gameObject.SetActive(false);
        }
        else
        {
            UIManager.Instance.ShowPopup<WarningPopup>().SetWarningPopup("수량을 확인하세요");
        }
    }
}
