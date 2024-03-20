using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttacks : MonoBehaviour
{
    [SerializeField] private GameObject _playerObj;
    private Player _player;

    private void Awake()
    {
        _player = _playerObj.GetComponent<Player>();
    }

    public void BaseAttack(AnimationEvent myEvent)
    {
        AttackInfoData attackInfoData = _player.Data.AttackData.GetAttackInfo(myEvent.intParameter);
        float range = attackInfoData.AttackRange;
        var forward = _player.transform.forward;
        forward.Normalize();
        Vector3 attackPos = _player.transform.position + new Vector3(0, 1.1f, 0) + forward * range;
        Collider[] colliders = Physics.OverlapSphere(attackPos, range);
        if (colliders.Length != 0)
        {
            foreach (Collider collider in colliders)
            {
                if (collider.GetComponent<IDamagable>() != null && collider.gameObject != _playerObj)
                {
                    collider.GetComponent<IDamagable>().TakePhysicalDamage(attackInfoData.Damage);
                }
            }
        }
    }
}
