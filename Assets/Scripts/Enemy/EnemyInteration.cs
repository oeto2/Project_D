using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyInteration : MonoBehaviour, IInteractable
{
    private Enemy _enemy;
    private bool _isRoot;
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
        //최초 1회 실행
        if(!_isRoot)
        {
            int monsterId = _enemy.Data.id;
            int rand = Random.Range(1, Database.Monster.Get(monsterId).monsterMaxRoot);

            for (int i = 0; i < rand; i++)
            {
                Debug.Log(Database.DropPer.GetItem(Database.Monster.Get(monsterId).dropId).itemName);
            }

            _isRoot = true;
        }

        //아이템루트 열기
        UIManager.Instance.ShowPopup<RewardPopup>();
    }
}
