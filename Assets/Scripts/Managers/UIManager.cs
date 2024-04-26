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

    //�θ� UI
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

    //�ش� �˾��� �����ϴ���
    public bool ExistPopup(string _key)
    {
        return _popups.ContainsKey(_key);
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

        //UI Popup SortOder ����
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

    //��ųʸ� �ʱ�ȭ
    public void ResetUIMangerData()
    {
        _popups.Clear();
        _interactPopus.Clear();
    }

    //Ui �����ϱ� 
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

    //��ȣ�ۿ� �˾� üũ
    public void CheckInteractPopup(string name_, GameObject popup_)
    {
        //��ȣ�ۿ� �˾� ����Ʈ �߰�
        switch (name_)
        {
            //��ȣ�ۿ� �˾� �߰�
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

    //Ȱ��ȭ �Ǿ��ִ� UIPopup ������� �ݱ�
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