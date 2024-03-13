using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTest : MonoBehaviour, IDamagable
{
    public float PlayerHP;

    //데미지 받기
    public void TakePhysicalDamage(int damageAmount)
    {
        PlayerHP -= damageAmount;
        Debug.Log("플레이어 공격 받음");
        Debug.Log($"플레이어의 체력 : {PlayerHP}");
    }
}
