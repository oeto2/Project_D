using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTest : MonoBehaviour, IDamagable
{
    public float PlayerHP;

    //������ �ޱ�
    public void TakePhysicalDamage(int damageAmount)
    {
        PlayerHP -= damageAmount;
        Debug.Log("�÷��̾� ���� ����");
        Debug.Log($"�÷��̾��� ü�� : {PlayerHP}");
    }
}
