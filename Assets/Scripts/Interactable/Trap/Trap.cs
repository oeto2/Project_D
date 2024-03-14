using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

public class Trap : MonoBehaviour
{
    public int damage;
    public float damageRate;
    public LayerMask player;

    //private IDamagable _iDamagable;

    private void OnTriggerEnter(Collider other)
    {
        if(player.value == (player.value| (1<<other.gameObject.layer)))
        {
            //_iDamageable에 플레이어의 IDamagable저장

            InvokeRepeating("DealDamage", 0, damageRate);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (player.value == (player.value | (1 << other.gameObject.layer)))
        {
            //_iDamagable =null;
            CancelInvoke("DealDamage");
        }
    }
    private void DealDamage()
    {
        //_iDamagable에서 대미지주는 함수를 불러옴.

    }
}
