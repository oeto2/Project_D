using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class BossInteraction : MonoBehaviour
{
    //플레이어 오브젝트 위치
    private Transform _playerTransform;

    //플레이어와의 거리
    private float _distance;

    //보스 체력관련
    private BossHpPopup _bossHpSctipt;
    [SerializeField] private GameObject _bossHpPopup;

    //적 스크립트
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

        //거리가 10이내라면
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

    //보스 체력 UI 세팅
    private void SetBossHpPopupInfo()
    {
        _bossHpPopup = _bossHpSctipt.gameObject;
        MonsterData monsterData = Database.Monster.Get(_enemy.Data.id);
        _bossHpSctipt.SetBossHpUI(monsterData.monsterHp, monsterData.monsterName);
        _bossHpPopup.SetActive(false);
    }
}
