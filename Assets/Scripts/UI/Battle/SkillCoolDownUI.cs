using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SkillCoolDownUI : MonoBehaviour
{
    [SerializeField] private List<slotCoolDowns> _slots;

    [System.Serializable]
    private struct slotCoolDowns
    {
        public GameObject gameObject;
        public TMP_Text text;
        public Image image;
    }

    private PlayerSkills _playerSkills;
    private PlayerSkillData _skillData;

    private void Start()
    {
        _playerSkills = GameManager.Instance.player.PlayerSkills;
        _skillData = GameManager.Instance.player.Data.SkillData;
        _playerSkills.coolDownUI += UpdateCoolDownUI;
    }

    private void UpdateCoolDownUI(int index)
    {
        float cool = _playerSkills.coolDowns[index];
        if (cool == 0)
        {
            _slots[index].gameObject.SetActive(false);
        }
        else
        {
            _slots[index].gameObject.SetActive(true);
            _slots[index].image.fillAmount = cool / _skillData.GetSkillInfo(index + 1).CoolDown;
            _slots[index].text.text = ((int)cool).ToString();
        }
    }
}
