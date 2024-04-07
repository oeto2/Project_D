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
        //상호작용 글씨
        return string.Format(_enemy.Data.monsterName);
    }

    public void OnInteract()
    {
        //이벤트 구독
        GameManager.Instance.GetRewardItemEvent += UpdateGetItemList;

        Debug.Log("상호작용 시작");

        //아이템루트 열기
        Reward reward = UIManager.Instance.GetPopup(nameof(RewardPopup)).GetComponent<Reward>();

        //최초 1회 실행
        if (!_isRoot)
        {
            int monsterId = _enemy.Data.id;
            int rand = Random.Range(1, Database.Monster.Get(monsterId).monsterMaxRoot);

            //리워드 창에 있는 아이템 비우기
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
            //리워드 창에 있는 아이템 비우기
            reward.CleanRewardItem();

            //리스트에 담겨있는 아이템 보여주기
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

    //리스트 비우기
    public void CleanMonsterList()
    {
        //이미 한번 수색을 했다면
    }
}
