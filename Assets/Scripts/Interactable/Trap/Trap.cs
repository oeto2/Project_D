using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

public class Trap : MonoBehaviour
{
    public int damage;
    //public float damageRate;
    //������ �Ǿƽĺ���������
    //public LayerMask player;

    //private IDamagable _iDamagable;

    private void OnTriggerEnter(Collider other)
    {
        other.gameObject.GetComponent<IDamagable>().TakePhysicalDamage(damage);
        //if(player.value == (player.value| (1<<other.gameObject.layer)))
        //{
        //    //_iDamageable�� �÷��̾��� IDamagable����
        //    _iDamagable = other.gameObject.GetComponent<IDamagable>();
        //    ////�޸� ��Ƹ���
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
    //    //_iDamagable���� ������ִ� �Լ��� �ҷ���.
    //    _iDamagable.TakePhysicalDamage(damage);
    //}
}
