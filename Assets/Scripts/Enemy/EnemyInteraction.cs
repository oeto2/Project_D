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
        //�̺�Ʈ ȣ�� �� : ȹ���� ������ ����Ʈ�� �޾ƿ�
        GameManager.Instance.GetRewardItemEvent += UpdateGetItemList;

        //�̺�Ʈ ȣ�� �� :���� �������� ������ ������ �̺�Ʈ�� ��� (��� ȹ��)
        GameManager.Instance.UpdateRewardCountEvent += UpdateGetItemCountList;
    }

    public void CancelInteract()
    {
    }

    public string GetInteractPrompt()
    {
        //��ȣ�ۿ� �۾�
        return string.Format(_enemy.Data.monsterName);
    }

    
    //óġ�� ���Ϳ� ��ȣ�ۿ� �� ȣ��Ǵ� �Լ�
    public void OnInteract()
    {
        //�����۷�Ʈ ����
        Reward reward = UIManager.Instance.GetPopup(nameof(RewardPopup)).GetComponent<Reward>();

        //���� 1ȸ ����
        if (!_isRoot)
        {
            int monsterId = _enemy.Data.id; //óġ�� ���� ID
            
            //Item � ȹ���� �� (����)
            int rand = Random.Range(1, Database.Monster.Get(monsterId).monsterMaxRoot); 

            //������ â�� �ִ� ������ ����
            reward.CleanRewardItem();

            //������̺��� �ٰŷ� ȹ���� ������ ID ���ͼ� ����Ʈ�� �߰�
            for (int i = 0; i < rand; i++)
            {
                ItemData getItem = Database.DropPer.GetItem(Database.Monster.Get(monsterId).dropId);
                reward.AcquireItem(getItem);
                getItemsID.Add(getItem.id);
            }

            UpdateGetItemCountList(); //ȹ���� ������ ���� 
            _isRoot = true;
        }
        else
        {
            //������ â�� �ִ� ������ ����
            reward.CleanRewardItem();

            //����Ʈ�� ����ִ� ������ �����ֱ�
            for (int i = 0; i < getItemsID.Count; i++)
            {
                if (getItemsCount == null)
                    reward.AcquireItem(Database.Item.Get(getItemsID[i]));

                else reward.AcquireItem(Database.Item.Get(getItemsID[i]), getItemsCount[i]);
            }
        }
    }

    //ȹ���� ������ ����Ʈ ������Ʈ
    public void UpdateGetItemList(List<int> itemsId_)
    {
        getItemsID = itemsId_;
    }

    //������ ���� ����Ʈ ������Ʈ
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
