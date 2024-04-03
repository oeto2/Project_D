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
        //상호작용 취소
    }

    public string GetInteractPrompt()
    {
        //상호작용 글씨
        return string.Format(_enemy.Data.monsterName);
    }

    public void OnInteract()
    {
        //아이템루트 열기
        UIManager.Instance.ShowPopup<RewardPopup>();
    }
}
