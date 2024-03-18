using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public interface IInteractable
{
    string GetInteractPrompt();
    void OnInteract();
    void CancelInteract();
}

public class InteractionManager : MonoBehaviour
{
    public InputActionReference interactionRef;
    public float checkRate = 0.05f;
    private float lastCheckTime;
    public float maxCheckDistance;
    public LayerMask layerMask;

    private GameObject _curInteractGameObject;
    private IInteractable _curInteractable;

    private Camera _camera;

    private bool _isInteract= false;
    public Slider loadingBar;
    public TextMeshProUGUI promptText;

    public static InteractionManager instance;

    private UIManager _uiManager;
    private interationPopup _interationPopup;

    private void Awake()
    {
        instance = this;
        _uiManager = UIManager.Instance;
    }

    // Start is called before the first frame update
    void Start()
    {
        _interationPopup = _uiManager.ShowPopup<interationPopup>(_uiManager.parentsUI);

        loadingBar = _interationPopup.LoadingBar;
        promptText = _interationPopup.PromptText;

        _camera = Camera.main;
        //_promptText = ;
        //_loadingBar = ;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time - lastCheckTime > checkRate)
        {
            lastCheckTime = Time.time;

            Ray ray = _camera.ScreenPointToRay(new Vector3(Screen.width *0.5f, Screen.height *0.5f, 0));
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, maxCheckDistance, layerMask))
            {
                if (hit.collider.gameObject != _curInteractGameObject)
                {
                    _curInteractGameObject = hit.collider.gameObject;
                    _curInteractable = hit.collider.GetComponent<IInteractable>();
                    SetPromptText();
                }
            }
            else
            {
                loadingBar.gameObject.SetActive(false);
                _isInteract = false;
                _curInteractGameObject = null;
                _curInteractable = null;
                promptText.gameObject.SetActive(false);
            }
        }
        if (_isInteract && _curInteractable != null)
        {
            _curInteractable.OnInteract();
        }
    }

    private void SetPromptText()
    {
        promptText.gameObject.SetActive(true);
        promptText.text = string.Format($"<b>[{interactionRef.action.GetBindingDisplayString()}]</b> {_curInteractable.GetInteractPrompt()}");
        
    }

    public void OnIteractInput(InputAction.CallbackContext callbackcontext)
    {
        if (callbackcontext.phase == InputActionPhase.Started && _curInteractable != null)
        {
            _isInteract = true;

        }
        else if (callbackcontext.phase == InputActionPhase.Canceled && _curInteractable != null)
        {
            _isInteract = false;
            _curInteractable.CancelInteract();
        }
    }
}
