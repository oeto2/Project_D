using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Treasure : MonoBehaviour,IInteractable
{
    private int _percentage;
    string IInteractable.GetInteractPrompt()
    {
        return string.Format("Open");
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
            //Instantiate("", transform.position, transform.rotation);
        }
    }
    void IInteractable.CancelInteract()
    {
        return;
    }
}
