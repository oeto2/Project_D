using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonOnMouse : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler
{
    [SerializeField] private TextMeshProUGUI _buttonText;

    //원래 색깔
    [SerializeField] private Color32 _originColor = new Color32(255, 255, 255, 255);
    //변경할 색깔
    [SerializeField] private Color32 _changeColor = new Color32(255, 220, 0, 255);
    
    public void OnPointerEnter(PointerEventData eventData)
    {
        _buttonText.color = _changeColor;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _buttonText.color = _originColor;
    }

    //기본 색으로 변경
    public void ChangeColor_Origin() => _buttonText.color = _originColor;
}