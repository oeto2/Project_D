using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public enum UIPopupType
{
    Normal, //기본
    Interact //상호작용
}

public abstract class UIBase : MonoBehaviour
{
    public UIPopupType uiPopupType = UIPopupType.Normal;
    
    [SerializeField] protected Button btnClose;
    
    private void Awake()
    {
        btnClose?.onClick.AddListener(() => CloseUI());
    }

    protected virtual void CloseUI()
    {
        if(btnClose != null)
        {
            gameObject.SetActive(false);
        }
    }
}




