using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Treasure : MonoBehaviour,IInteractable
{
    private int _percentage;
    private string _open = "Open";
    string IInteractable.GetInteractPrompt()
    {
        return _open;
    }

    void IInteractable.OnInteract()
    {
        //���� ����
        _percentage = Random.Range(0, 100);
        if(_percentage<70)
        {
            //����� ����
        }
        else
        {
            //�̹͵���
            
        }
    }
    void IInteractable.CancelInteract()
    {
        return;
    }
}
