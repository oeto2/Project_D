using DarkPixelRPGUI.Scripts.UI;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BossHpPopup : UIBase
{
    [SerializeField] private Slider _hpSlider;
    [SerializeField] private TMP_Text _nameText;
    private SliderTextValue _sliderTextValue;

    private void Awake()
    {
        _sliderTextValue = _hpSlider.GetComponent<SliderTextValue>();
    }

    //보스 체력 UI 세팅
    public void SetBossHpUI(float maxHp_, string bossName_)
    {
        _hpSlider.maxValue = maxHp_;
        _hpSlider.value = maxHp_;
        _nameText.text = bossName_;

        _sliderTextValue.UpdateText(maxHp_);
    }

    //보스 체력 갱신
    public void UpdateBossHpUI(float curHp_)
    {
        _sliderTextValue.UpdateText(curHp_);
        _hpSlider.value = curHp_;
    }
}
