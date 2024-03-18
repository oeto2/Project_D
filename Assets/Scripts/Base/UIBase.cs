using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public abstract class UIBase : MonoBehaviour
{
    [SerializeField] private Button btnClose;

    private void Awake()
    {
        CloseUI();
    }

    protected virtual void CloseUI()
    {
        btnClose.onClick.AddListener(() => CloseUI());
        gameObject.SetActive(false);
    }
}