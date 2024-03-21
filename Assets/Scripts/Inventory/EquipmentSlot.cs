using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class EquipmentSlot : MonoBehaviour,IPointerClickHandler, IBeginDragHandler, IDragHandler, IEndDragHandler, IDropHandler
{
    public ItemData item; // 획득한 아이템.
    public Image itemImage; // 아이템의 이미지.

    private void Awake()
    {
        ClearSlot();
    }

    // 이미지의 투명도 조절.
    private void SetColor(float _alpha)
    {
        Color color = itemImage.color;
        color.a = _alpha;
        itemImage.color = color;
    }

    // 아이템 획득
    public void AddItem(ItemData _item)
    {
        item = _item;
        itemImage.sprite = item.Sprite;

        SetColor(1);
    }

    // 슬롯 초기화.
    public void ClearSlot()
    {
        item = null;
        itemImage.sprite = null;
        SetColor(0);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            if (item != null)
            {
                if (item.itemType == Constants.ItemType.Weapon)
                {
                    //무기 장착
                }
                else if (item.itemType == Constants.ItemType.Equip)
                {
                    //장비장착
                }
            }
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (item != null)
        {
            DragSlot.instance.equipmentSlot = this;
            DragSlot.instance.DragSetImage(itemImage);
            DragSlot.instance.dragItem = item;
            DragSlot.instance.transform.position = eventData.position;
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (item != null)
        {
            DragSlot.instance.transform.position = eventData.position;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        DragSlot.instance.SetColor(0);
        DragSlot.instance.dragItem = null;
        DragSlot.instance.dragSlot = null;
        DragSlot.instance.equipmentSlot = null;
        DragSlot.instance.weaponSlot = null;
    }

    public void OnDrop(PointerEventData eventData)
    {
        //if문에서 장비인지 확인
        if (DragSlot.instance.dragSlot != null && DragSlot.instance.dragItem.itemType == Constants.ItemType.Equip)
            ChangeSlot();
    }

    private void ChangeSlot()
    {
        ItemData _tempItem = item;

        AddItem(DragSlot.instance.dragSlot.item);

        if (_tempItem != null)
            DragSlot.instance.dragSlot.AddItem(_tempItem);
        else
            DragSlot.instance.dragSlot.ClearSlot();
    }
}
