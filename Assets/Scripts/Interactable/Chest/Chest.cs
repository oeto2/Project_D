using System.Collections;
using System.Collections.Generic;
using UnityEditor;
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
                //아이템루트 열기
                Reward reward = UIManager.Instance.GetPopup(nameof(RewardPopup)).GetComponent<Reward>();

                _animator.SetTrigger(_openParameterName);

                //상호작용 UI 숨기기
                HideInteractUI();

                //이후에 상자 ID로 수정하기
                int chestId = 11000001;
                int rand = Random.Range(1, Database.Monster.Get(chestId).monsterMaxRoot);

                //리워드 창에 있는 아이템 비우기
                reward.CleanRewardItem();

                for (int i = 0; i < rand; i++)
                {
                    ItemData getItem = Database.DropPer.GetItem(Database.Monster.Get(chestId).dropId);
                    reward.AcquireItem(getItem);
                    _getItemsID.Add(getItem.id);
                }

                _isOpen = true;
            }
        }

        //상자에서 아이템 꺼내기
        else
        {
            //아이템루트 열기
            Reward reward = UIManager.Instance.GetPopup(nameof(RewardPopup)).GetComponent<Reward>();

            //리워드 창에 있는 아이템 비우기
            reward.CleanRewardItem();

            //리스트에 담겨있는 아이템 보여주기
            for (int i = 0; i < _getItemsID.Count; i++)
            {
                reward.AcquireItem(Database.Item.Get(_getItemsID[i]));
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
    }

    private void HideInteractUI()
    {
        _loadingBar.gameObject.SetActive(false);
        _time = 0;
        _loadingBar.value = _time / 3;
    }
}
