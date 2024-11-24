using System.Collections.Generic;
using UnityEngine;

public class EnemyInteraction : MonoBehaviour, IInteractable
{
    private Enemy _enemy;
    [SerializeField] public bool _isRoot;
    public List<RewardInfo> rewardInfo;
    public List<int> getItemsID;
    public List<int> getItemsCount;

    private void Awake()
    {
        _enemy = GetComponentInParent<Enemy>();
    }

    public void CancelInteract()
    {
    }

    public string GetInteractPrompt()
    {
        //상호작용 글씨
        return string.Format(_enemy.Data.monsterName);
    }

    public void OnInteract()
    {
        //이벤트 구독
        GameManager.Instance.GetRewardItemEvent += UpdateGetItemList;

        //아이템루트 열기
        Reward reward = UIManager.Instance.GetPopup(nameof(RewardPopup)).GetComponent<Reward>();

        //최초 1회 실행
        if (!_isRoot)
        {
            GameManager.Instance.UpdateRewardCountEvent += UpdateGetItemCountList;
            int monsterId = _enemy.Data.id;
            int rand = Random.Range(1, Database.Monster.Get(monsterId).monsterMaxRoot);

            //리워드 창에 있는 아이템 비우기
            reward.CleanRewardItem();

            for (int i = 0; i < rand; i++)
            {
                ItemData getItem = Database.DropPer.GetItem(Database.Monster.Get(monsterId).dropId);
                reward.AcquireItem(getItem);
                getItemsID.Add(getItem.id);
            }

            UpdateGetItemCountList();
            _isRoot = true;
        }
        else
        {
            GameManager.Instance.UpdateRewardCountEvent += UpdateGetItemCountList;
            //리워드 창에 있는 아이템 비우기
            reward.CleanRewardItem();

            //리스트에 담겨있는 아이템 보여주기
            for (int i = 0; i < getItemsID.Count; i++)
            {
                if (getItemsCount == null)
                    reward.AcquireItem(Database.Item.Get(getItemsID[i]));

                else reward.AcquireItem(Database.Item.Get(getItemsID[i]), getItemsCount[i]);
            }
        }
    }

    public void UpdateGetItemList(List<int> itemsId_)
    {
        getItemsID = itemsId_;
    }

    //아이템 갯수 리스트 업데이트
    private void UpdateGetItemCountList()
    {
        getItemsCount.Clear();
        getItemsID.Clear();
        Reward reward = UIManager.Instance.GetPopupObject(nameof(RewardPopup)).GetComponent<Reward>();

        for (int i = 0; i < reward.slots.Length; i++)
        {
            if (reward.slots[i].itemCount != 0)
            {
                getItemsCount.Add(reward.slots[i].itemCount);
                getItemsID.Add(reward.slots[i].item.id);
            }
        }
    }
}
