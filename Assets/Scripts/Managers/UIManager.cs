using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class UIManager : SingletonBase<UIManager>
{
    private const string _dragPopupName = "DragPopup";
    private const string _optionPopupName = "OptionPopup";
    private const string _gameEndPopupName = "GameEndPopup";
    private const string _battleUiPopupName = "BattleUI";

    //부모 UI
    public Transform parentsUI = null;
    private Dictionary<string, UIBase> _popups = new Dictionary<string, UIBase>();
    [SerializeField] private List<UIBase> _interactPopus = new List<UIBase>();

    public int BattleUICount;

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

        //UI Popup SortOder 정리
        ArrageUIpopup_Oder(popupName, obj);

        return ShowPopup(obj, popupName);
    }

    public UIBase ShowPopup(GameObject obj, string popupname)
    {
        var popup = obj.GetComponent<UIBase>();
        _popups.Add(popupname, popup);
        obj.SetActive(true);
        CheckInteractPopup(popupname, obj);
        return popup;
    }

    public void ShowPopup(GameObject obj)
    {
        obj.SetActive(true);
    }

    //딕셔너리 초기화
    public void ResetUIMangerData()
    {
        _popups.Clear();
        _interactPopus.Clear();
    }

    //Ui 정렬하기 
    private void ArrageUIpopup_Oder(string popupName_, GameObject popup_)
    {
        switch (popupName_)
        {
            case _battleUiPopupName:
                popup_.GetComponent<Canvas>().sortingOrder = 0;
                break;

            case _gameEndPopupName:
                popup_.GetComponent<Canvas>().sortingOrder = 2;
                break;

            case _dragPopupName:
                popup_.GetComponent<Canvas>().sortingOrder = 20;
                break;

            case _optionPopupName:
                popup_.GetComponent<Canvas>().sortingOrder = 100;
                break;

            default:
                popup_.GetComponent<Canvas>().sortingOrder = _popups.Count;
                break;
        }
    }

    //상호작용 팝업 체크
    public void CheckInteractPopup(string name_, GameObject popup_)
    {
        //상호작용 팝업 리스트 추가
        switch (name_)
        {
            //상호작용 팝업 추가
            case nameof(InventoryPopup):
                _interactPopus.Add(popup_.GetComponent<InventoryPopup>());
                break;

            case nameof(EquipmentPopup):
                _interactPopus.Add(popup_.GetComponent<EquipmentPopup>());
                break;

            case nameof(RewardPopup):
                _interactPopus.Add(popup_.GetComponent<RewardPopup>());
                break;
        }
    }

    //활성화 되어있는 UIPopup 순서대로 닫기
    public void CloseActiveUI()
    {
        for(int i = 0; i< _interactPopus.Count; i++)
        {
            if (_interactPopus[i].gameObject.active)
            {
                _interactPopus[i].gameObject.SetActive(false);
                return;
            }
        }
    }
}