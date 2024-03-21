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
    [SerializeField] private Button _enterButton;
    
    private void Start()
    {
        _PlayerHealth = GameManager.Instance.playerObject.GetComponent<Health>();
        
        //�÷��̾� ����� DeadUI ����
        _PlayerHealth.OnDie += ShowPlayerDeadUI;
        _PlayerHealth.OnDie += StartShowGameOverPanel;

        //�÷��̾� �ǰݽ� ü�� UI ���ΰ�ħ
        _PlayerHealth.OnDamage += RefreshPlayerHpUI;
    }

    //ü�� UI ���ΰ�ħ
    private void RefreshPlayerHpUI(int damage)
    {
        _playerHp.value = Mathf.Clamp01(_PlayerHealth.health / _PlayerHealth.maxHealth);
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
        Cursor.lockState = CursorLockMode.None;
        _gameEndUIPanel.SetActive(true);
    }
}
