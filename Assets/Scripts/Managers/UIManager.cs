using System.Collections.Generic;
using UnityEngine;

public class UIManager : SingletonBase<UIManager>
{
    //�θ� UI
    public Transform parentsUI = null;
    private Dictionary<string, UIBase> _popups = new Dictionary<string, UIBase>();

    //ESCŰ�� ���� �� �ִ� �˾� ����
    public Stack<UIBase> interactPopups = new Stack<UIBase>();

    private void Awake()
    {
        GameManager.Instance.SceneLoadEvent += ResetUIMangerData;
    }

    public GameObject GetPopup(string popupName)
    {
        ShowPopup(popupName);

        return _popups[popupName].gameObject;
    }

    public GameObject GetPopupObject(string popupName)
    {
        if (!_popups.ContainsKey(popupName))
        {
            return null;
        }

        return _popups[popupName].gameObject;
    }

    //�ش� �˾��� �����ϴ���
    public bool ExistPopup(string _key)
    {
        return _popups.ContainsKey(_key);
    }

    public T ShowPopup<T>(Transform parents = null) where T : UIBase
    {
        return ShowPopup(typeof(T).Name, parents) as T;
    }

    //�˾� �ҷ�����
    public UIBase ShowPopup(string popupname, Transform parents = null)
    {
        var obj = Resources.Load("Popups/" + popupname, typeof(GameObject)) as GameObject;
        if (!obj)
        {
            Debug.LogWarning($"Failed to ShowPopup({popupname})");
            return null;
        }

        //�̹� ����Ʈ�� �ش� �˾��� �����Ѵٸ� return
        if (_popups.ContainsKey(popupname))
        {
            ShowPopup(_popups[popupname].gameObject);
            return null;
        }

        return ShowPopupWithPrefab(obj, popupname, parents);
    }

    public UIBase ShowPopupWithPrefab(GameObject prefab, string popupName, Transform parents = null)
    {
        string name = popupName;
        var obj = Instantiate(prefab, parents);
        obj.name = name;

        return ShowPopup(obj, popupName);
    }

    public UIBase ShowPopup(GameObject obj, string popupname)
    {
        var popup = obj.GetComponent<UIBase>();
        _popups.Add(popupname, popup);
        obj.SetActive(true);
        CheckPopupType(popup); //�˾� Ÿ�� üũ
        return popup;
    }

    public void ShowPopup(GameObject obj)
    {
        CheckPopupType(obj.GetComponent<UIBase>()); //�˾� Ÿ�� üũ
        obj.SetActive(true);
    }

    //��ųʸ� �ʱ�ȭ
    public void ResetUIMangerData()
    {
        _popups.Clear();
        interactPopups.Clear();
    }

    //PopupType üũ
    public void CheckPopupType(UIBase popup_)
    {
        switch (popup_.uiPopupType)
        {
            //��ȣ �ۿ� �˾��̶�� ���� ����
            case UIPopupType.Interact:
                interactPopups.Push(popup_);
                break;
        }
    }

    //Ȱ��ȭ �Ǿ��ִ� UIPopup ������� �ݱ�
    public void CloseActiveUI()
    {
        if (interactPopups.Count > 0)
        {
            UIBase popup = interactPopups.Pop();
            
            //�ش� �˾��� Ȱ��ȭ ���̶��
            if (popup.gameObject.activeSelf)
            {
                //�ش� �˾��� ����
                popup.gameObject.SetActive(false);
            }
            //�ش� �˾��� ��Ȱ��ȭ ���̿��ٸ�
            else
            {
                CloseActiveUI();
            }
        }

        //�����ִ� �˾��� �������� ������, Ŀ�� ���
        if (interactPopups.Count <= 0)
            Cursor.lockState = CursorLockMode.Locked;
    }

    //�˾��� ���� �������� Ȯ���ϱ�
    public void CheckCloseAllPopup()
    {
        foreach (var popup in _popups)
        {
            if (popup.Value != null)
            {
                //�˾��� �Ѱ��� Ȱ��ȭ ���̸� return
                if (popup.Value.gameObject.activeSelf)
                {
                    //����ó�� : ��Ʋ, ��ȣ�ۿ� UI�� Ȱ��ȭ ���̿��� ���� x
                    if (popup.Value.gameObject.name is nameof(BattleUI) or nameof(interationPopup)
                        or nameof(DragPopup))
                    {
                        continue;
                    }
                    return;
                }
            }
        }

        //Queue ����ֱ�
        interactPopups.Clear();
        //Ŀ�� ���
        Cursor.lockState = CursorLockMode.Locked;
    }
}