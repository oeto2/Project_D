using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class Mimic : MonoBehaviour, IInteractable
{
    private UIManager _uiManager;
    private interationPopup _interationPopup;
    private float _time;
    private Slider _loadingBar;
    [SerializeField] private Animator _animator;

    [SerializeField] private Enemy _enemy;
    [SerializeField] private CharacterController _characterController;
    [SerializeField] private NavMeshAgent _navMeshAgent;
    [SerializeField] private BoxCollider _boxColider;

    //���ڸ� ��������
    private bool _isOpen;

    private void Awake()
    {
        _uiManager = UIManager.Instance;
        _animator = GetComponent<Animator>();
        _enemy = GetComponent<Enemy>();
        _characterController = GetComponent<CharacterController>();
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _boxColider = GetComponent<BoxCollider>();
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
        if (!_isOpen)
        {
            _loadingBar.gameObject.SetActive(true);
            _time += Time.deltaTime;
            _loadingBar.value = _time / 3;

            //��ȣ�ۿ� �Ϸ�
            if (_time >= 3)
            {
                //��ȣ�ۿ� UI �����
                HideInteractUI();
                ActiveMimic();
                _isOpen = true;
                Debug.Log("�̹� ����");
            }
        }
    }

    void IInteractable.CancelInteract()
    {
        _loadingBar.gameObject.SetActive(false);
        _time = 0;
        _loadingBar.value = _time / 3;
    }

    private void HideInteractUI()
    {
        _loadingBar.gameObject.SetActive(false);
        _time = 0;
        _loadingBar.value = _time / 3;
    }

    //�̹� Ȱ��ȭ
    private void ActiveMimic()
    {
        _enemy.enabled = true;
        _characterController.enabled = true;
        _navMeshAgent.enabled = true;
        _boxColider.enabled = false;
        this.gameObject.layer = 6;
    }
}
