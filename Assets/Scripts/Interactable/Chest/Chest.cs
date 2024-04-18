using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEditorInternal.Profiling.Memory.Experimental;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.InputSystem.LowLevel.InputStateHistory;

public class Chest : MonoBehaviour, IInteractable
{
    private UIManager _uiManager;
    private interationPopup _interationPopup;
    private float _time;
    private Slider _loadingBar;
    [SerializeField] private Animator _animator;

    //상자를 열었는지
    private bool _isOpen;
    private const string _openParameterName = "Open";

    //상자에서 얻는 아이템들
    public List<int> _getItemsID;
    //아이템의 슬롯
    public List<int> _getItemsCount;

    private void Awake()
    {
        _uiManager = UIManager.Instance;
        _animator = GetComponentInChildren<Animator>();
    }

    private void Start()
    {
        _interationPopup = _uiManager.GetPopup(nameof(interationPopup)).GetComponent<interationPopup>();

        _loadingBar = _interationPopup.LoadingBar;
    }

    string IInteractable.GetInteractPrompt()
    {
        _time = 0;
        return string.Format("Use Chest");
    }

    void IInteractable.OnInteract()
    {
        //이벤트 구독
        GameManager.Instance.GetRewardItemEvent += UpdateGetItemList;

        //상자 처음 열기
        if (!_isOpen)
        {
            _loadingBar.gameObject.SetActive(true);
            _time += Time.deltaTime;
            _loadingBar.value = _time / 3;

            //상호작용 완료
            if (_time >= 3)
            {
                GameManager.Instance.CloseRewardPopupEvent += UpdateGetItemCountList;

                //아이템루트 열기
                Reward reward = UIManager.Instance.GetPopup(nameof(RewardPopup)).GetComponent<Reward>();

                _animator.SetTrigger(_openParameterName);

                //상호작용 UI 숨기기
                HideInteractUI();

                int chestId = 20000001;
                //상자의 MaxRoot가 존재하지 않아서 5로 설정
                int rand = Random.Range(1, 6);

                //리워드 창에 있는 아이템 비우기
                reward.CleanRewardItem();
                //아이템 슬롯 정보 비우기
                _getItemsCount.Clear();

                for (int i = 0; i < rand; i++)
                {
                    //현재 탐색중인 슬롯
                    ItemData getItem = Database.DropPer.GetItem(chestId);
                    //최초 1회 실행
                    reward.AcquireItem(getItem);

                    _getItemsID.Add(getItem.id);
                    _getItemsCount.Add(reward.GetItemCountInSlot(i));
                }
                _isOpen = true;
            }
        }

        //상자에서 아이템 꺼내기
        else
        {
            GameManager.Instance.CloseRewardPopupEvent += UpdateGetItemCountList;

            //아이템루트 열기
            Reward reward = UIManager.Instance.GetPopup(nameof(RewardPopup)).GetComponent<Reward>();

            //리워드 창에 있는 아이템 비우기
            reward.CleanRewardItem();

            //리스트에 담겨있는 아이템 보여주기
            for (int i = 0; i < _getItemsID.Count; i++)
            {
                if (_getItemsCount == null)
                    reward.AcquireItem(Database.Item.Get(_getItemsID[i]));

                else reward.AcquireItem(Database.Item.Get(_getItemsID[i]), _getItemsCount[i]);
            }
        }
    }
    void IInteractable.CancelInteract()
    {
        _loadingBar.gameObject.SetActive(false);
        _time = 0;
        _loadingBar.value = _time / 3;
    }

    public void UpdateGetItemList(List<int> itemsId_)
    {
        _getItemsID = itemsId_;

        Reward reward = UIManager.Instance.GetPopup(nameof(RewardPopup)).GetComponent<Reward>();
    }

    private void HideInteractUI()
    {
        _loadingBar.gameObject.SetActive(false);
        _time = 0;
        _loadingBar.value = _time / 3;
    }

    //아이템 갯수 리스트 업데이트
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
