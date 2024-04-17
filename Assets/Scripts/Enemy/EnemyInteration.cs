using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyInteration : MonoBehaviour, IInteractable
{
    private Enemy _enemy;
    [SerializeField] private bool _isRoot;
    public List<int> _getItemsID;
    public List<int> _getItemsCount;
    private void Awake()
    {
        _enemy = GetComponentInParent<Enemy>();
    }

    public void CancelInteract()
    {
    }

    public string GetInteractPrompt()
    {
        //��ȣ�ۿ� �۾�
        return string.Format(_enemy.Data.monsterName);
    }

    public void OnInteract()
    {
        //�̺�Ʈ ����
        GameManager.Instance.GetRewardItemEvent += UpdateGetItemList;

        Debug.Log("��ȣ�ۿ� ����");

        //�����۷�Ʈ ����
        Reward reward = UIManager.Instance.GetPopup(nameof(RewardPopup)).GetComponent<Reward>();

        //���� 1ȸ ����
        if (!_isRoot)
        {
            GameManager.Instance.CloseRewardPopupEvent += UpdateGetItemCountList;
            int monsterId = _enemy.Data.id;
            int rand = Random.Range(1, Database.Monster.Get(monsterId).monsterMaxRoot);

            //������ â�� �ִ� ������ ����
            reward.CleanRewardItem();

            for (int i = 0; i < rand; i++)
            {
                ItemData getItem = Database.DropPer.GetItem(Database.Monster.Get(monsterId).dropId);
                reward.AcquireItem(getItem);
                _getItemsID.Add(getItem.id);
                _getItemsCount.Add(reward.GetItemCountInSlot(i));
            }

            _isRoot = true;
        }
        else
        {
            GameManager.Instance.CloseRewardPopupEvent += UpdateGetItemCountList;
            //������ â�� �ִ� ������ ����
            reward.CleanRewardItem();

            //����Ʈ�� ����ִ� ������ �����ֱ�
            for (int i = 0; i < _getItemsID.Count; i++)
            {
                if (_getItemsCount == null)
                    reward.AcquireItem(Database.Item.Get(_getItemsID[i]));

                else reward.AcquireItem(Database.Item.Get(_getItemsID[i]), _getItemsCount[i]);
            }
        }
    }

    public void UpdateGetItemList(List<int> itemsId_)
    {
        _getItemsID = itemsId_;
    }

    //������ ���� ����Ʈ ������Ʈ
    private void UpdateGetItemCountList()
    {
        _getItemsCount.Clear();
        _getItemsID.Clear();
        Reward reward = UIManager.Instance.GetPopupObject(nameof(RewardPopup)).GetComponent<Reward>();

        for (int i = 0; i < reward.slots.Length; i++)
        {
            if (reward.slots[i].itemCount != 0)
            {
                _getItemsCount.Add(reward.slots[i].itemCount);
                _getItemsID.Add(reward.slots[i].item.id);
            }
        }
    }
}
