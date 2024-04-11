using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyInteration : MonoBehaviour, IInteractable
{
    private Enemy _enemy;
    [SerializeField] private bool _isRoot;
    public List<int> _getItemsID;
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
            int monsterId = _enemy.Data.id;
            int rand = Random.Range(1, Database.Monster.Get(monsterId).monsterMaxRoot);

            //������ â�� �ִ� ������ ����
            reward.CleanRewardItem();

            for (int i = 0; i < rand; i++)
            {
                ItemData getItem = Database.DropPer.GetItem(Database.Monster.Get(monsterId).dropId);
                reward.AcquireItem(getItem);
                _getItemsID.Add(getItem.id);
            }

            _isRoot = true;
        }
        else
        {
            //������ â�� �ִ� ������ ����
            reward.CleanRewardItem();

            //����Ʈ�� ����ִ� ������ �����ֱ�
            for (int i = 0; i < _getItemsID.Count; i++)
            {
                reward.AcquireItem(Database.Item.Get(_getItemsID[i]));
            }
        }
    }

    public void UpdateGetItemList(List<int> itemsId_)
    {
        _getItemsID = itemsId_;
    }

    //����Ʈ ����
    public void CleanMonsterList()
    {
        //�̹� �ѹ� ������ �ߴٸ�
    }
}
