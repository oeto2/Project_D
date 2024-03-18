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
        //상자 오픈
        _percentage = Random.Range(0, 100);
        if(_percentage<70)
        {
            //평범한 상자
        }
        else
        {
            //미믹등장
            
        }
    }
    void IInteractable.CancelInteract()
    {
        return;
    }
}
