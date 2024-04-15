using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapAnimationController : MonoBehaviour
{
    private Animator _animator;
    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("충돌");
        _animator.SetBool("Attack", true);
    }
    //private void OnTriggerStay(Collider other)
    //{
    //    Debug.Log("충돌중");
    //    _animator.SetBool("Attack", true);
    //}
    private void OnTriggerExit(Collider other)
    {
        Debug.Log("해제");
        _animator.SetBool("Attack", false);
    }
}
