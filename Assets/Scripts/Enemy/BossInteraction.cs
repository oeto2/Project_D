using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class BossInteraction : MonoBehaviour
{
    //�÷��̾� ������Ʈ ��ġ
    private Transform _playerTransform;

    //�÷��̾���� �Ÿ�
    private float _distance;

    //���� ü�°���
    private BossHpPopup _bossHpSctipt;
    [SerializeField] private GameObject _bossHpPopup;

    //�� ��ũ��Ʈ
    private Enemy _enemy;

    private void Start()
    {
        _playerTransform = GameManager.Instance.playerObject.transform;
        _enemy = GetComponent<Enemy>();

        _bossHpSctipt = UIManager.Instance.GetPopup(nameof(BossHpPopup)).GetComponent<BossHpPopup>();
        SetBossHpPopupInfo();

        _enemy.TakeDamageEvent += _bossHpSctipt.UpdateBossHpUI;
    }

    private void Update()
    {
        _distance = Vector3.Distance(transform.position, _playerTransform.position);

        //�Ÿ��� 10�̳����
        if (_distance <= 15)
        {
            if (!_bossHpPopup.active)
            {
                _bossHpPopup.SetActive(true);
            }
        }
        else
        {

            if (_bossHpPopup.active)
                _bossHpPopup.SetActive(false);
        }
    }

    //���� ü�� UI ����
    private void SetBossHpPopupInfo()
    {
        _bossHpPopup = _bossHpSctipt.gameObject;
        MonsterData monsterData = Database.Monster.Get(_enemy.Data.id);
        _bossHpSctipt.SetBossHpUI(monsterData.monsterHp, monsterData.monsterName);
        _bossHpPopup.SetActive(false);
    }
}
