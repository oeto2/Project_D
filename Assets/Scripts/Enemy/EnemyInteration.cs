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
        return string.Format("[E] ���̷���");
    }

    public void OnInteract()
    {
        //�����۷�Ʈ ����
    }
}
