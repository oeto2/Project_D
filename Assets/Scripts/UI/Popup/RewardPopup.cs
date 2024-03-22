using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RewardPopup : UIBase
{
    private void OnEnable()
    {
        UIManager.Instance.BattleUICount++;
    }

    private void OnDisable()
    {
        UIManager.Instance.BattleUICount--;
    }
}
