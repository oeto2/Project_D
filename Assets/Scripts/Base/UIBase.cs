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

            //UI가 모두 종료 되었으면 다시 커서 락
            if (UIManager.Instance.BattleUICount <= 0)
                Cursor.lockState = CursorLockMode.Locked;
        }
    }
}