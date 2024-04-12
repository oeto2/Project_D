using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
        //���� ó�� ����
        if(!_isOpen)
        {
            _loadingBar.gameObject.SetActive(true);
            _time += Time.deltaTime;
            _loadingBar.value = _time / 3;

            //��ȣ�ۿ� �Ϸ�
            if (_time >= 3)
            {
                _animator.SetTrigger(_openParameterName);
                _isOpen = true;

                _loadingBar.gameObject.SetActive(false);
                _time = 0;
                _loadingBar.value = _time / 3;

                UIManager.Instance.ShowPopup<RewardPopup>();
                UIManager.Instance.ShowPopup<InventoryPopup>();
            }
        }

        //���ڿ��� ������ ������
        else
        {

        }
    }
    void IInteractable.CancelInteract()
    {
        _loadingBar.gameObject.SetActive(false);
        _time = 0;
        _loadingBar.value = _time / 3;
    }
}
