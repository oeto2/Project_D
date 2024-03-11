using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.InputAction;


public interface IInteractable
{
    string GetInteractPrompt();
    void OnInteract();
    void CancelInteract();
}


public class InteractionManager : MonoBehaviour
{
    public float checkRate = 0.05f;
    private float lastCheckTime;
    public float maxCheckDistance;
    public LayerMask layerMask;

    private GameObject curInteractGameObject;
    private IInteractable curInteractable;

    public TextMeshProUGUI promptText;
    private Camera camera;

    private bool _isInteract= false;
    private GameObject _loadingBar;

    // Start is called before the first frame update
    void Start()
    {
        camera = Camera.main;
        _loadingBar = UIManager.instance.loadingBar.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time - lastCheckTime > checkRate)
        {
            lastCheckTime = Time.time;

            Ray ray = camera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, maxCheckDistance, layerMask))
            {
                if (hit.collider.gameObject != curInteractGameObject)
                {
                    curInteractGameObject = hit.collider.gameObject;
                    curInteractable = hit.collider.GetComponent<IInteractable>();
                    SetPromptText();
                }
            }
            else
            {
                _loadingBar.SetActive(false);
                _isInteract = false;
                curInteractGameObject = null;
                curInteractable = null;
                promptText.gameObject.SetActive(false);
            }
        }
        if (_isInteract && curInteractable != null)
        {
            curInteractable.OnInteract();
        }
    }

    private void SetPromptText()
    {
        promptText.gameObject.SetActive(true);
        promptText.text = string.Format("<b>[E]</b> {0}", curInteractable.GetInteractPrompt());
        
    }

    //매프레임 입력을 받지 않음
    public void OnIteractInput(InputAction.CallbackContext callbackcontext)
    {
        if (callbackcontext.phase == InputActionPhase.Started && curInteractable != null)
        {
            _isInteract = true;
            //curInteractable.OnInteract();
            //curInteractGameObject = null;
            //curInteractable = null;
            //promptText.gameObject.SetActive(false);
        }
        else if (callbackcontext.phase == InputActionPhase.Canceled && curInteractable != null)
        {
            _isInteract = false;
            curInteractable.CancelInteract();
        }
    }
}
