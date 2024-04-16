using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WarningPopup : UIBase
{
    public TextMeshProUGUI warningText;
    public void SetWarningPopup(string text_)
    {
        warningText.text = text_;
    }
}
