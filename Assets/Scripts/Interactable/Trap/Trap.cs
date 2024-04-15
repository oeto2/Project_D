using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

public class Trap : MonoBehaviour
{
    public int damage;
    //public float damageRate;
    //함정이 피아식별하지않음
    //public LayerMask player;

    //private IDamagable _iDamagable;

    private void OnTriggerEnter(Collider other)
    {
        other.gameObject.GetComponent<IDamagable>().TakePhysicalDamage(damage);
        //if(player.value == (player.value| (1<<other.gameObject.layer)))
        //{
        //    //_iDamageable에 플레이어의 IDamagable저장
        //    _iDamagable = other.gameObject.GetComponent<IDamagable>();
        //    ////메모리 잡아먹음
        //    //InvokeRepeating("DealDamage", 0, damageRate);
        //    //StartCoroutine(DealDamage());
        //}
    }
    //private void OnTriggerExit(Collider other)
    //{
    //    //if (player.value == (player.value | (1 << other.gameObject.layer)))
    //    //{
    //    //    _iDamagable =null;
    //    //    //CancelInvoke("DealDamage");
    //    //    //StopCoroutine(DealDamage());
    //    //}
    //}
    //IEnumerator DealDamage()
    //{
    //    while(true)
    //    {
    //        _iDamagable.TakePhysicalDamage(damage);
    //        yield return new WaitForSeconds(damageRate);
    //    }
        
    //}
    //private void DealDamage()
    //{
    //    //_iDamagable에서 대미지주는 함수를 불러옴.
    //    _iDamagable.TakePhysicalDamage(damage);
    //}
}
