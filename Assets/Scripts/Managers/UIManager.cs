using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : SingletonBase<UIManager>
{
    private const string _dragPopupName = "DragPopup";
    private const string _optionPopupName = "OptionPopup";
    //부모 UI
    public Transform parentsUI = null;
    private Dictionary<string, UIBase> _popups = new Dictionary<string, UIBase>();

    public int BattleUICount;

    private void Awake() => _isLoad = false;
    public GameObject GetPopup(string popupName)
    {
        if (!_popups.ContainsKey(popupName))
        {
            ShowPopup(popupName);
        }

        return _popups[popupName].gameObject;
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

    public T ShowPopup<T>(Transform parents = null) where T : UIBase
    {
        return ShowPopup(typeof(T).Name, parents) as T;
    }

    public UIBase ShowPopupWithPrefab(GameObject prefab, string popupName, Transform parents = null)
    {
        if (parentsUI != null)
            parents = parentsUI;

        string name = popupName;
        var obj = Instantiate(prefab, parents);
        obj.name = name;

        switch (obj.name)
        {
            case _dragPopupName:
                obj.GetComponent<Canvas>().sortingOrder = 20;
                break;

            case _optionPopupName:
                obj.GetComponent<Canvas>().sortingOrder = 100;
                break;

            default:
                obj.GetComponent<Canvas>().sortingOrder = _popups.Count;
                break;
        }

        return ShowPopup(obj, popupName);
    }

    public UIBase ShowPopup(GameObject obj, string popupname)
    {
        var popup = obj.GetComponent<UIBase>();
        _popups.Add(popupname, popup);

        obj.SetActive(true);
        return popup;
    }

    public void ShowPopup(GameObject obj)
    {
        obj.SetActive(true);
    }
}