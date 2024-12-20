using TMPro;
using UnityEngine;

public class CharacterStatsUI : MonoBehaviour
{
    private Player _player;
    [SerializeField] private TMP_Text _healthText;
    [SerializeField] private TMP_Text _manaText;
    [SerializeField] private TMP_Text _staminaText;
    [SerializeField] private TMP_Text _attackText;
    [SerializeField] private TMP_Text _defenseText;

    private void Awake()
    {
        _player = GameManager.Instance.playerObject.GetComponent<Player>();
        UpdateUI();
    }

    private void Update()
    {
        UpdateUI();
    }

    private void UpdateUI()
    {
        _healthText.text = $"{(int)_player.Stats.health} / {(int)_player.Stats.maxHealth}";
        _manaText.text = $"{(int)_player.Stats.mana} / {(int)_player.Stats.maxMana}";
        _staminaText.text = $"{(int)_player.Stats.stamina} / {(int)_player.Stats.maxStamina}";
        _attackText.text = $"{(int)_player.Stats.attack}";
        _defenseText.text = $"{(int)_player.Stats.curDefense}";
    }
}
