using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class QuickSlot : MonoBehaviour, IDropHandler
{
    public Slot slot;

    public Image itemImage; // 아이템의 이미지.
    //public int itemOriginPosX;
    //public int itemOriginPosY;

    // 필요한 컴포넌트.
    [SerializeField]
    private TextMeshProUGUI _textCount;
    [SerializeField]
    private GameObject _countImage;
    public void OnDrop(PointerEventData eventData)
    {
        if(DragSlot.instance.dragSlot.item.itemType == Constants.ItemType.Consume)
        {
            slot = DragSlot.instance.dragSlot;
            SetQuickSlot();
        }
    }

    private void SetColor(float alpha_)
    {
        Color color = itemImage.color;
        color.a = alpha_;
        itemImage.color = color;
    }

    public void SetQuickSlot()
    {
        itemImage.sprite = slot.item.Sprite;
        _countImage.SetActive(true);
        _textCount.text = slot.itemCount.ToString();
        SetColor(alpha_: 1);
    }
    public void SetSlotCount()
    {
        _textCount.text = slot.itemCount.ToString();
        if (slot.itemCount <= 0)
            ClearSlot();
    }

    public void ClearSlot()
    {
        slot = null;
        itemImage.sprite = null;
        SetColor(alpha_: 0);

        _textCount.text = "0";
        _countImage.SetActive(false);
    }
    public void UseItem()
    {
        if (slot.item == null)
        {
            ClearSlot();
            return;
        }
        slot.UseItem();
    }
}
