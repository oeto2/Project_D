using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonOnMouse : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler
{
    [SerializeField] private TextMeshProUGUI _buttonText;

    //���� ����
    [SerializeField] private Color32 _originColor = new Color32(255, 255, 255, 255);
    //������ ����
    [SerializeField] private Color32 _changeColor = new Color32(255, 220, 0, 255);
    
    public void OnPointerEnter(PointerEventData eventData)
    {
        _buttonText.color = _changeColor;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _buttonText.color = _originColor;
    }

    //�⺻ ������ ����
    public void ChangeColor_Origin() => _buttonText.color = _originColor;
}