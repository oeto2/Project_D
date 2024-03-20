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

        //�÷��̾� �ǰݽ� ü�� UI ���ΰ�ħ
        _player.TakeDamageEvent += RefreshPlayerHpUI;
        //�÷��̾� ����� DeadUI ����
        _player.PlayerDieEvent += ShowPlayerDeadUI;
    }

    //ü�� UI ���ΰ�ħ
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
