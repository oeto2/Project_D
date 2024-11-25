using System.Collections.Generic;
using UnityEngine;

public class UIManager : SingletonBase<UIManager>
{
    //부모 UI
    public Transform parentsUI = null;
    private Dictionary<string, UIBase> _popups = new Dictionary<string, UIBase>();

    //ESC키로 닫을 수 있는 팝업 모음
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

    //해당 팝업이 존재하는지
    public bool ExistPopup(string _key)
    {
        return _popups.ContainsKey(_key);
    }

    public T ShowPopup<T>(Transform parents = null) where T : UIBase
    {
        return ShowPopup(typeof(T).Name, parents) as T;
    }

    //팝업 불러오기
    public UIBase ShowPopup(string popupname, Transform parents = null)
    {
        var obj = Resources.Load("Popups/" + popupname, typeof(GameObject)) as GameObject;
        if (!obj)
        {
            Debug.LogWarning($"Failed to ShowPopup({popupname})");
            return null;
        }

        //이미 리스트에 해당 팝업이 존재한다면 return
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
        CheckPopupType(popup); //팝업 타입 체크
        return popup;
    }

    public void ShowPopup(GameObject obj)
    {
        CheckPopupType(obj.GetComponent<UIBase>()); //팝업 타입 체크
        obj.SetActive(true);
    }

    //딕셔너리 초기화
    public void ResetUIMangerData()
    {
        _popups.Clear();
        interactPopups.Clear();
    }

    //PopupType 체크
    public void CheckPopupType(UIBase popup_)
    {
        switch (popup_.uiPopupType)
        {
            //상호 작용 팝업이라면 따로 보관
            case UIPopupType.Interact:
                interactPopups.Push(popup_);
                break;
        }
    }

    //활성화 되어있는 UIPopup 순서대로 닫기
    public void CloseActiveUI()
    {
        if (interactPopups.Count > 0)
        {
            UIBase popup = interactPopups.Pop();
            
            //해당 팝업이 활성화 중이라면
            if (popup.gameObject.activeSelf)
            {
                //해당 팝업을 닫음
                popup.gameObject.SetActive(false);
            }
            //해당 팝업이 비활성화 중이였다면
            else
            {
                CloseActiveUI();
            }
        }

        //열려있는 팝업이 존재하지 않으면, 커서 잠금
        if (interactPopups.Count <= 0)
            Cursor.lockState = CursorLockMode.Locked;
    }

    //팝업이 전부 닫혔는지 확인하기
    public void CheckCloseAllPopup()
    {
        foreach (var popup in _popups)
        {
            if (popup.Value != null)
            {
                //팝업이 한개라도 활성화 중이면 return
                if (popup.Value.gameObject.activeSelf)
                {
                    //예외처리 : 배틀, 상호작용 UI는 활성화 중이여도 문제 x
                    if (popup.Value.gameObject.name is nameof(BattleUI) or nameof(interationPopup)
                        or nameof(DragPopup))
                    {
                        continue;
                    }
                    return;
                }
            }
        }

        //Queue 비워주기
        interactPopups.Clear();
        //커서 잠금
        Cursor.lockState = CursorLockMode.Locked;
    }
}