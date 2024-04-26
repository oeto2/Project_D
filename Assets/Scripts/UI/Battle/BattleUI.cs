using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class BattleUI : UIBase
{
    private CharacterStats _PlayerHealth;
    private Player _Player;

    [SerializeField] private Slider _playerHp;
    [SerializeField] private Slider _playerMp;
    [SerializeField] private Slider _playerSta;
    [SerializeField] private GameObject _playerDeadUIPanel;
    [SerializeField] private GameObject _gameEndUIPanel;
    [SerializeField] private Button _enterButton;
    [SerializeField] private TMP_Text _cannotSkillText;

    private WaitForSeconds _waitForSeconds = new WaitForSeconds(1f);

    public QuickSlot[] quickSlot;
    
    private void Start()
    {
        _PlayerHealth = GameManager.Instance.playerObject?.GetComponent<CharacterStats>();
        _Player = GameManager.Instance.playerObject?.GetComponent<Player>();

        //플레이어 사망시 DeadUI 띄우기
        _PlayerHealth.OnDie += ShowPlayerDeadUI;
        _PlayerHealth.OnDie += StartShowGameOverPanel;

        //플레이어 피격시 체력 UI 새로고침
        _PlayerHealth.OnDamage += RefreshPlayerHpUI;
        _PlayerHealth.OnMana += RefreshPlayerMpUI;
        _PlayerHealth.OnStamina += RefreshPlayerStaUI;

        _PlayerHealth.noManaText = NoManaText;
        _PlayerHealth.coolDownText = CoolDownText;
    }

    //체력 UI 새로고침
    private void RefreshPlayerHpUI(int damage)
    {
        _playerHp.value = Mathf.Clamp01(_PlayerHealth.health / _PlayerHealth.maxHealth);
    }

    private void RefreshPlayerMpUI(float amount)
    {
        _playerMp.value = Mathf.Clamp01(_PlayerHealth.mana / _PlayerHealth.maxMana);
    }

    private void RefreshPlayerStaUI(float amount)
    {
        _playerSta.value = Mathf.Clamp01(_PlayerHealth.stamina / _PlayerHealth.maxStamina);
    }

    private void ShowPlayerDeadUI()
    {
        _playerDeadUIPanel.SetActive(true);
    }

    private void StartShowGameOverPanel()
    {
        StartCoroutine(ShowGameOverPanel());
    }

    private IEnumerator ShowGameOverPanel()
    {
        yield return new WaitForSeconds(5f);
        UIManager.Instance.ShowPopup<GameEndPopup>();
    }

    private void NoManaText(float cost)
    {
        StopCoroutine(TextOff());
        _cannotSkillText.enabled = true;
        _cannotSkillText.text = $"마나가 부족합니다. (필요 마나 : {(int)cost})";
        StartCoroutine(TextOff());
    }

    private void CoolDownText(float time)
    {
        StopCoroutine(TextOff());
        _cannotSkillText.enabled = true;
        _cannotSkillText.text = $"스킬이 쿨다운 중입니다. (남은 시간 : {(int)time}초)";
        StartCoroutine(TextOff());
    }

    IEnumerator TextOff()
    {
        yield return _waitForSeconds;
        _cannotSkillText.enabled = false;
    }
}
