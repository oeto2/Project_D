using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEditorInternal.Profiling.Memory.Experimental;
using UnityEngine;
using UnityEngine.SceneManagement;
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

    //������ ������̺� ID
    [SerializeField] private int _dropId;

    //Ʃ�丮�󿡼� ȹ�� �����۵�
    [SerializeField] private List<int> _tutorialGetItems;

    //���ڿ��� ��� �����۵�
    public List<int> _getItemsID;
    //�������� ����
    public List<int> _getItemsCount;

    //���� ���� �̸�
    private string currenSceneName;

    private void Awake()
    {
        _uiManager = UIManager.Instance;
        _animator = GetComponentInChildren<Animator>();
    }

    private void Start()
    {
        _interationPopup = _uiManager.GetPopup(nameof(interationPopup)).GetComponent<interationPopup>();

        _loadingBar = _interationPopup.LoadingBar;

        currenSceneName = SceneManager.GetActiveScene().name;
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

                //Ʃ�丮�� ���� ���
                #region Ʃ�丮�� ����
                if (currenSceneName == "TutorialScene")
                {
                    //Ʃ�丮�� ������ Ǯ��Ʈ �ֱ�
                    for (int i = 0; i < _tutorialGetItems.Count; i++)
                    {
                        reward.AcquireItem(Database.Item.Get(_tutorialGetItems[i]));
                        _getItemsID.Add(_tutorialGetItems[i]);
                        _getItemsCount.Add(reward.GetItemCountInSlot(i));
                    }

                    _isOpen = true;
                    return;
                }
                #endregion

                GameManager.Instance.CloseRewardPopupEvent += UpdateGetItemCountList;
                _animator.SetTrigger(_openParameterName);

                //��ȣ�ۿ� UI �����
                HideInteractUI();

                //������ MaxRoot�� �������� �ʾƼ� 5�� ����
                int rand = Random.Range(1, 6);

                //������ â�� �ִ� ������ ����
                reward.CleanRewardItem();
                //������ ���� ���� ����
                _getItemsCount.Clear();

                for (int i = 0; i < rand; i++)
                {
                    //���� Ž������ ����
                    ItemData getItem = Database.DropPer.GetItem(_dropId);
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
