using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyInteration : MonoBehaviour, IInteractable
{
    public void CancelInteract()
    {
        //��ȣ�ۿ� ���
    }

    public string GetInteractPrompt()
    {
        //��ȣ�ۿ� �۾�
        return string.Format("���̷���");
    }

    public void OnInteract()
    {
        //�����۷�Ʈ ����
        UIManager.Instance.ShowPopup<RewardPopup>();
    }
}
