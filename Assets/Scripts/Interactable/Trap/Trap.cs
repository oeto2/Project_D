using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

public class Trap : MonoBehaviour
{
    public int damage;

    private void OnTriggerEnter(Collider other)
    {
        other.gameObject.GetComponent<IDamagable>().TakePhysicalDamage(damage);
    }

}
