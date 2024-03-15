using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;


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
    private GameObject _loadingBar;
    private TextMeshProUGUI _promptText;

    // Start is called before the first frame update
    void Start()
    {
        _camera = Camera.main;
        _promptText = UIManager.Instance.promptText;
        _loadingBar = UIManager.Instance.loadingBar.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time - lastCheckTime > checkRate)
        {
            lastCheckTime = Time.time;

            Ray ray = _camera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
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
                _loadingBar.SetActive(false);
                _isInteract = false;
                _curInteractGameObject = null;
                _curInteractable = null;
                _promptText.gameObject.SetActive(false);
            }
        }
        if (_isInteract && _curInteractable != null)
        {
            _curInteractable.OnInteract();
        }
    }

    private void SetPromptText()
    {
        _promptText.gameObject.SetActive(true);
        _promptText.text = string.Format($"<b>[{interactionRef.action.GetBindingDisplayString()}]</b> {_curInteractable.GetInteractPrompt()}");
        
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
