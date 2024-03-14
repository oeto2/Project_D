using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    private Enemy _enemy;
    private Transform _playerObject;

    private void Awake()
    {
        _enemy = GetComponentInParent<Enemy>();
        _playerObject = GameManager.Instance.playerObject.transform;
    }

    //플레이어 공격
    public void StartAttack()
    {
        Ray ray = new Ray(transform.position, (_playerObject.position - transform.position).normalized);
        RaycastHit hitData;

        //레이캐스트 사용
        Physics.Raycast(ray, out hitData, _enemy.Data.AttackRange);

        if(hitData.transform?.tag == "Player")
        {
            hitData.transform?.GetComponent<IDamagable>().TakePhysicalDamage(_enemy.Data.Damage);
        }
    }
}
