using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Experimental.GraphView.GraphView;

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

    public QuickSlot quickSlot1;
    public QuickSlot quickSlot2;
    public QuickSlot quickSlot3;
    
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
}
