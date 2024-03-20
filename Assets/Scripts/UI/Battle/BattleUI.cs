using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleUI : UIBase
{
    private Health _PlayerHealth;

    [SerializeField] private Slider _playerHp;
    [SerializeField] private Slider _playerMp;
    [SerializeField] private GameObject _playerDeadUIPanel;
    [SerializeField] private GameObject _gameEndUIPanel;
    
    private void Awake()
    {
        _PlayerHealth = GameManager.Instance.playerObject.GetComponent<Health>();
        
        //플레이어 사망시 DeadUI 띄우기
        _PlayerHealth.OnDie += ShowPlayerDeadUI;
        _PlayerHealth.OnDie += StartShowGameOverPanel;
    }
    private void Start()
    {
        //플레이어 피격시 체력 UI 새로고침
        _PlayerHealth.OnDamage += RefreshPlayerHpUI;
    }

    //체력 UI 새로고침
    private void RefreshPlayerHpUI(int damage)
    {
        Debug.Log("체력 UI 갱신");
        _playerHp.value = Mathf.Clamp01(_PlayerHealth.health / _PlayerHealth.maxHealth);
        Debug.Log(_PlayerHealth.health / _PlayerHealth.maxHealth);
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
        _gameEndUIPanel.SetActive(true);
    }
}
