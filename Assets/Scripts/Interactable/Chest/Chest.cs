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

    //���ڸ� ��������
    private bool _isOpen;
    private const string _openParameterName = "Open";

    //���ڿ��� ��� �����۵�
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
                //�����۷�Ʈ ����
                Reward reward = UIManager.Instance.GetPopup(nameof(RewardPopup)).GetComponent<Reward>();

                _animator.SetTrigger(_openParameterName);

                //��ȣ�ۿ� UI �����
                HideInteractUI();

                //���Ŀ� ���� ID�� �����ϱ�
                int chestId = 11000001;
                int rand = Random.Range(1, Database.Monster.Get(chestId).monsterMaxRoot);

                //������ â�� �ִ� ������ ����
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

        //���ڿ��� ������ ������
        else
        {
            //�����۷�Ʈ ����
            Reward reward = UIManager.Instance.GetPopup(nameof(RewardPopup)).GetComponent<Reward>();

            //������ â�� �ִ� ������ ����
            reward.CleanRewardItem();

            //����Ʈ�� ����ִ� ������ �����ֱ�
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
