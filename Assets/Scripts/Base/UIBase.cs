using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public abstract class UIBase : MonoBehaviour
{
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