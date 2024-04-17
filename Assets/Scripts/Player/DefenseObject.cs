using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefenseObject : MonoBehaviour, IDamagable
{
    public void TakePhysicalDamage(int damageAmount)
    {
        SoundManager.Instance.PlayGuardSound();
    }
}
