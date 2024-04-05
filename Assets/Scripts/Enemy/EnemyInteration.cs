using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyInteration : MonoBehaviour, IInteractable
{
    private Enemy _enemy;

    private void Awake()
    {
        _enemy = GetComponentInParent<Enemy>();
    }

    public void CancelInteract()
    {
        //��ȣ�ۿ� ���
    }

    public string GetInteractPrompt()
    {
        //��ȣ�ۿ� �۾�
        return string.Format(_enemy.Data.monsterName);
    }

    public void OnInteract()
    {
        //�����۷�Ʈ ����
        UIManager.Instance.ShowPopup<RewardPopup>();
    }
}
