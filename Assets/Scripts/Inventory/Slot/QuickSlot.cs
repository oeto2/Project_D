using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using static UnityEditor.Experimental.AssetDatabaseExperimental.AssetDatabaseCounters;

public class QuickSlot : MonoBehaviour, IDropHandler
{
    public Slot slot;

    public Image itemImage; // �������� �̹���.
    //public int itemOriginPosX;
    //public int itemOriginPosY;

    // �ʿ��� ������Ʈ.
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
        SetColor(1);
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
        SetColor(0);

        _textCount.text = "0";
        _countImage.SetActive(false);
    }
    public void UseItem()
    {
        slot.UseItem();
        SetSlotCount();
    }
}