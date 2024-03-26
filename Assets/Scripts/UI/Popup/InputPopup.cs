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
        int sellItemCount_ = int.Parse(sellItemCount.text);
        if (sellSlot.itemCount - sellItemCount_ >= 0)
        {
            sellSlot.AddItem(sellSlot.item, sellSlot.itemCount - sellItemCount_);
            this.gameObject.SetActive(false);
        }
        else
        {
            UIManager.Instance.ShowPopup<WarningPopup>();
        }
    }
}
