using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleUI : UIBase
{
    private Player _player;
    private PlayerSO _playerData;

    [SerializeField] private Slider _playerHp;
    [SerializeField] private Slider _playerMp;
    [SerializeField] private GameObject _playerDeadUIPanel;

    private void Awake()
    {
        _player = GameManager.Instance.playerObject.GetComponent<Player>();
        _playerData = _player.Data;

        //플레이어 피격시 체력 UI 새로고침
        _player.TakeDamageEvent += RefreshPlayerHpUI;
        //플레이어 사망시 DeadUI 띄우기
        _player.PlayerDieEvent += ShowPlayerDeadUI;
    }

    //체력 UI 새로고침
    private void RefreshPlayerHpUI()
    {
        _playerHp.value = Mathf.Clamp01(_playerData.CurHealth / _playerData.MaxHealth);
        Debug.Log(_playerData.CurHealth / _playerData.MaxHealth);
    }

    private void ShowPlayerDeadUI()
    {
        _playerDeadUIPanel.SetActive(true);
    }
}
