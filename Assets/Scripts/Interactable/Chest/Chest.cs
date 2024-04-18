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

    //���ڸ� ��������
    private bool _isOpen;
    private const string _openParameterName = "Open";

    //���ڿ��� ��� �����۵�
    public List<int> _getItemsID;
    //�������� ����
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
        //�̺�Ʈ ����
        GameManager.Instance.GetRewardItemEvent += UpdateGetItemList;

        //���� ó�� ����
        if (!_isOpen)
        {
            _loadingBar.gameObject.SetActive(true);
            _time += Time.deltaTime;
            _loadingBar.value = _time / 3;

            //��ȣ�ۿ� �Ϸ�
            if (_time >= 3)
            {
                GameManager.Instance.CloseRewardPopupEvent += UpdateGetItemCountList;

                //�����۷�Ʈ ����
                Reward reward = UIManager.Instance.GetPopup(nameof(RewardPopup)).GetComponent<Reward>();

                _animator.SetTrigger(_openParameterName);

                //��ȣ�ۿ� UI �����
                HideInteractUI();

                int chestId = 20000001;
                //������ MaxRoot�� �������� �ʾƼ� 5�� ����
                int rand = Random.Range(1, 6);

                //������ â�� �ִ� ������ ����
                reward.CleanRewardItem();
                //������ ���� ���� ����
                _getItemsCount.Clear();

                for (int i = 0; i < rand; i++)
                {
                    //���� Ž������ ����
                    ItemData getItem = Database.DropPer.GetItem(chestId);
                    //���� 1ȸ ����
                    reward.AcquireItem(getItem);

                    _getItemsID.Add(getItem.id);
                    _getItemsCount.Add(reward.GetItemCountInSlot(i));
                }
                _isOpen = true;
            }
        }

        //���ڿ��� ������ ������
        else
        {
            GameManager.Instance.CloseRewardPopupEvent += UpdateGetItemCountList;

            //�����۷�Ʈ ����
            Reward reward = UIManager.Instance.GetPopup(nameof(RewardPopup)).GetComponent<Reward>();

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
