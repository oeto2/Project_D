using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyInteration : MonoBehaviour, IInteractable
{
    public void CancelInteract()
    {
        //상호작용 취소
    }

    public string GetInteractPrompt()
    {
        //상호작용 글씨
        return string.Format("스켈레톤");
    }

    public void OnInteract()
    {
        //아이템루트 열기
        UIManager.Instance.ShowPopup<RewardPopup>();
    }
}
