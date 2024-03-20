using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleUI : UIBase
{
    private Player _player;

    [SerializeField] private Slider _playerHp;
    [SerializeField] private Slider _playerMp;
    [SerializeField] private GameObject _playerDeadUIPanel;
    [SerializeField] private GameObject _gameEndUIPanel;
    
    private void Awake()
    {
        _player = GameManager.Instance.playerObject.GetComponent<Player>();
        //플레이어 피격시 체력 UI 새로고침
        _player.TakeDamageEvent += RefreshPlayerHpUI;
        //플레이어 사망시 DeadUI 띄우기
        _player.PlayerDieEvent += ShowPlayerDeadUI;
        _player.PlayerDieEvent += StartShowGameOverPanel;
    }

    //체력 UI 새로고침
    private void RefreshPlayerHpUI()
    {
        _playerHp.value = Mathf.Clamp01(_player.PlayerCurHp / _player.PlayerMaxHp);
        Debug.Log(_player.PlayerCurHp / _player.PlayerMaxHp);
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
