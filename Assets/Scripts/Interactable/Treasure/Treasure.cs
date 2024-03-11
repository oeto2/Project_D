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
        //상자 오픈
        _percentage = Random.Range(0, 100);
        if(_percentage<70)
        {
            //평범한 상자
        }
        else
        {
            //미믹등장
            //Instantiate("", transform.position, transform.rotation);
        }
    }
    void IInteractable.CancelInteract()
    {
        return;
    }
}
