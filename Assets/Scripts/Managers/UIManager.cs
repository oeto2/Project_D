using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : SingletonBase<UIManager>
{
    //부모 UI
    public Transform parentsUI = null;
    private Dictionary<string,UIBase> popups = new Dictionary<string,UIBase>();

    private void Awake() => _isLoad = false;
    public GameObject GetPopup(string popupName)
    {
        if (!popups.ContainsKey(popupName))
        {
            ShowPopup(popupName);
        }

        return popups[popupName].gameObject;
    }

    //팝업 불러오기
    public UIBase ShowPopup(string popupname, Transform parents = null)
    {
        //이미 리스트에 해당 팝업이 존재한다면 return
        if(popups.ContainsKey(popupname))
        {
            ShowPopup(popups[popupname].gameObject);
            return null;
        }

        var obj = Resources.Load("Popups/" + popupname, typeof(GameObject)) as GameObject;
        if (!obj)
        {
            Debug.LogWarning($"Failed to ShowPopup({popupname})");
            return null;
        }
        return ShowPopupWithPrefab(obj, popupname, parents);
    }

    public T ShowPopup<T>(Transform parents = null) where T : UIBase
    {
        return ShowPopup(typeof(T).Name, parents) as T;
    }

    public UIBase ShowPopupWithPrefab(GameObject prefab, string popupName , Transform parents = null)
    {
        string name = popupName;
        var obj = Instantiate(prefab, parents);
        obj.name = name;

        obj.GetComponent<Canvas>().sortingOrder = popups.Count;

        ////캔버스 부착
        //Canvas canvas = obj.AddComponent<Canvas>();
        //CanvasScaler canvasScaler = obj.AddComponent<CanvasScaler>();

        ////캔버스 세팅
        //canvas.renderMode = RenderMode.ScreenSpaceOverlay;
        //canvas.sortingOrder = popups.Count;

        ////캔버스 스케일러 세팅
        //canvasScaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
        //canvasScaler.referenceResolution = new Vector2(1920, 1080);
        //canvasScaler.screenMatchMode = CanvasScaler.ScreenMatchMode.MatchWidthOrHeight;
        //canvasScaler.referencePixelsPerUnit = 100;

        return ShowPopup(obj, popupName);
    }

    public UIBase ShowPopup(GameObject obj, string popupname)
    {
        var popup = obj.GetComponent<UIBase>();
        popups.Add(popupname, popup);

        obj.SetActive(true);
        return popup;
    }

    public void ShowPopup(GameObject obj)
    {
        obj.SetActive(true);
    }
}