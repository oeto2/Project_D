using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

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

    private void Start()
    {
        //이벤트 호출 시 : 획득할 아이템 리스트를 받아옴
        GameManager.Instance.GetRewardItemEvent += UpdateGetItemList;

        //이벤트 호출 시 :현재 보유중인 아이템 갯수를 이벤트에 등록 (모두 획득)
        GameManager.Instance.UpdateRewardCountEvent += UpdateGetItemCountList;
    }

    public void CancelInteract()
    {
    }

    public string GetInteractPrompt()
    {
        //상호작용 글씨
        return string.Format(_enemy.Data.monsterName);
    }

    
    //처치한 몬스터와 상호작용 시 호출되는 함수
    public void OnInteract()
    {
        //아이템루트 열기
        Reward reward = UIManager.Instance.GetPopup(nameof(RewardPopup)).GetComponent<Reward>();

        //최초 1회 실행
        if (!_isRoot)
        {
            int monsterId = _enemy.Data.id; //처치한 몬스터 ID
            
            //Item 몇개 획득할 지 (랜덤)
            int rand = Random.Range(1, Database.Monster.Get(monsterId).monsterMaxRoot); 

            //리워드 창에 있는 아이템 비우기
            reward.CleanRewardItem();

            //드랍테이블을 근거로 획득할 아이템 ID 얻어와서 리스트에 추가
            for (int i = 0; i < rand; i++)
            {
                ItemData getItem = Database.DropPer.GetItem(Database.Monster.Get(monsterId).dropId);
                reward.AcquireItem(getItem);
                getItemsID.Add(getItem.id);
            }

            UpdateGetItemCountList(); //획득한 아이템 갯수 
            _isRoot = true;
        }
        else
        {
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

    //획득할 아이템 리스트 업데이트
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
