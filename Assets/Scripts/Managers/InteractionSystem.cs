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

public class InteractionSystem : MonoBehaviour
{
    public InputActionReference interactionRef;
    public float checkRate = 0.05f;
    private float lastCheckTime;
    public float maxCheckDistance;
    public LayerMask layerMask;

    private GameObject _curInteractGameObject;
    public IInteractable curInteractable;

    private Camera _camera;

    public bool isInteract = false;
    public Slider loadingBar;
    public TextMeshProUGUI promptText;

    private UIManager _uiManager;
    public interationPopup _interationPopup;

    private void Awake()
    {
        _uiManager = UIManager.Instance;
    }

    // Start is called before the first frame update
    void Start()
    {
        _camera = Camera.main;
        _interationPopup = _uiManager.GetPopup(nameof(interationPopup)).GetComponent<interationPopup>();
        loadingBar = _interationPopup.LoadingBar;
        promptText = _interationPopup.PromptText;        
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
                //부딪힌 오브젝트가 현재 상호작용중인 오브젝트가 아니라면
                if (hit.collider.gameObject != _curInteractGameObject)
                {
                    _curInteractGameObject = hit.collider.gameObject;
                    curInteractable = hit.collider.GetComponent<IInteractable>();
                    SetPromptText();
                    Debug.Log("레이캐스트 포탈 발견");
                }
            }
            else
            {
                loadingBar.gameObject.SetActive(false);
                isInteract = false;
                _curInteractGameObject = null;
                curInteractable = null;
                promptText.gameObject.SetActive(false);
            }
        }
        if (isInteract && curInteractable != null)
        {
            curInteractable.OnInteract();
        }
    }

    private void SetPromptText()
    {
        promptText.gameObject.SetActive(true);
        promptText.text = string.Format($"<b>[{interactionRef.action.GetBindingDisplayString()}]</b> {curInteractable.GetInteractPrompt()}");
        
    }

    // 지훈님의 추억
    //public void OnIteractInput(InputAction.CallbackContext callbackcontext)
    //{
    //    if (callbackcontext.phase == InputActionPhase.Started && curInteractable != null)
    //    {
    //        isInteract = true;

    //    }
    //    else if (callbackcontext.phase == InputActionPhase.Canceled && curInteractable != null)
    //    {
    //        isInteract = false;
    //        curInteractable.CancelInteract();
    //    }
    //}
}
